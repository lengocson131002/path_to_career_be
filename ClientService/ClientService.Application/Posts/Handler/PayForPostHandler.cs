using AutoMapper;
using ClientService.Application.Common.Constants;
using ClientService.Application.Transactions.Models;
using ClientService.Domain.Entities;

namespace ClientService.Application.Posts.Handler;

public class PayForPostHandler : IRequestHandler<PayForPostRequest, TransactionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IMapper _mapper;

    public PayForPostHandler(IUnitOfWork unitOfWork, ICurrentAccountService currentAccountService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _currentAccountService = currentAccountService;
        _mapper = mapper;
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

        return _mapper.Map<TransactionResponse>(transaction);
    }
}