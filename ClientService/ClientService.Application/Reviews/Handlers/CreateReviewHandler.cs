using AutoMapper;
using ClientService.Application.Reviews.Commands;
using ClientService.Application.Reviews.Models;
using ClientService.Domain.Entities;

namespace ClientService.Application.Reviews.Handlers;

public class CreateReviewHandler : IRequestHandler<CreateReviewRequest, ReviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public CreateReviewHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<ReviewResponse> Handle(CreateReviewRequest request, CancellationToken cancellationToken)
    {
        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(acc => acc.Id == request.AccountId);
        var account = accountQuery.FirstOrDefault();
        if (account == null || !Role.Freelancer.Equals(account.Role))
        {
            throw new ApiException(ResponseCode.AccountErrorNotFound);
        }
        
        var currentAccount = await _currentAccountService.GetCurrentAccount();

        if (currentAccount.Id == request.AccountId)
        {
            throw new ApiException(ResponseCode.ReviewErrorCannotReviewYourself);
        }
        // check exist review
        var reviewQuery = await _unitOfWork.ReviewRepository.GetAsync(
            review => review.AccountId == request.AccountId && review.ReviewerId == currentAccount.Id);
        if (reviewQuery.Any())
        {
            throw new ApiException(ResponseCode.ReviewErrorExisted);
        }
        
        var review = new Review()
        {
            ReviewerId = currentAccount.Id,
            AccountId = request.AccountId,
            Score = request.Score,
            Content = request.Content
        };

        await _unitOfWork.ReviewRepository.AddAsync(review);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ReviewResponse>(review);
    }
}