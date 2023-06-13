using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClientService.Application.Transactions.Commands;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Transactions.Handlers;

public class ConfirmTransactionHandler : IRequestHandler<ConfirmTransactionRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public ConfirmTransactionHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<StatusResponse> Handle(ConfirmTransactionRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.TransactionRepository.GetAsync(
            predicate: transaction => transaction.Id == request.Id,
            includes: new ListResponse<Expression<Func<Transaction, object>>>()
            {
                transaction => transaction.Post
            });

        var transaction = await query.FirstOrDefaultAsync(cancellationToken);
        if (transaction == null || !TransactionStatus.New.Equals(transaction.Status))
        {
            throw new ApiException(ResponseCode.TransactionNotFound);
        }

        transaction.Status = TransactionStatus.Completed;
        await _unitOfWork.TransactionRepository.UpdateAsync(transaction);
        
        var post = transaction.Post;
        if (post == null)
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }
        
        post.Status = PostStatus.Paid;
        
        await _unitOfWork.PostRepository.UpdateAsync(post);
        await _unitOfWork.SaveChangesAsync();
        
        // Notify for user
        var userNotification = new Notification(NotificationType.TransactionConfirmed)
        {
            AccountId = post.AccountId,
            Data = JsonSerializer.Serialize(post, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }),
            ReferenceId = post.Id.ToString(),
        };
        await _notificationService.PushNotification(userNotification);
        
        // Notify for freelancer
        var freelancerQuery = await _unitOfWork.AccountRepository.GetAsync(acc => Role.Freelancer.Equals(acc.Role));
        var freelancers = await freelancerQuery.ToListAsync(cancellationToken);
        foreach (var freelancer in freelancers)
        {
            var notification = new Notification(NotificationType.PostCreated)
            {
                AccountId = freelancer.Id,
                Data = JsonSerializer.Serialize(post, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }),
                ReferenceId = post.Id.ToString(),
            };
            await _notificationService.PushNotification(notification);
        }
        
        return new StatusResponse(true);
    }
}