using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ClientService.Application.Common.Constants;
using ClientService.Application.Transactions.Models;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Posts.Handler;

public class PayForPostHandler : IRequestHandler<PayForPostRequest, TransactionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public PayForPostHandler(IUnitOfWork unitOfWork, ICurrentAccountService currentAccountService, IMapper mapper, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _currentAccountService = currentAccountService;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<TransactionResponse> Handle(PayForPostRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        var post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
        if (post == null || post.AccountId != currentAccount.Id || !PostStatus.New.Equals(post.Status))
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }

        var transaction = new Transaction()
        {
            Account = currentAccount,
            PayMethod = request.Method,
            Amount = ServicePrice.ServicePrices[post.ServiceType],
            Content = $"{post.ServiceType}-{post.Id}".ToUpper()
        };

        await _unitOfWork.TransactionRepository.AddAsync(transaction);

        post.Transaction = transaction;
        await _unitOfWork.PostRepository.UpdateAsync(post);
        
        // Save changes
        await _unitOfWork.SaveChangesAsync();
        
        // Notify for admin to confirm
        var adminQuery = await _unitOfWork.AccountRepository.GetAsync(acc => Role.Admin.Equals(acc.Role));
        var admins = await adminQuery.ToListAsync(cancellationToken);

        foreach (var admin in admins)
        {
            var notification = new Notification(NotificationType.TransactionCreated)
            {
                AccountId = admin.Id,
                Data = JsonSerializer.Serialize(transaction, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }),
                ReferenceId = transaction.Id.ToString(),
            };
            await _notificationService.PushNotification(notification);
        }

        return _mapper.Map<TransactionResponse>(transaction);
    }
}