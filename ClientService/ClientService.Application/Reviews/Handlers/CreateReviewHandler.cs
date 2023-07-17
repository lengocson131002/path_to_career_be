using AutoMapper;
using ClientService.Application.Common.Persistence;
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
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        var post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
        if (post == null || currentAccount.Id != post.AccountId)
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }

        if (!post.CanReview)
        {
            throw new ApiException(ResponseCode.InvalidPostStatus);
        }
        
        // check exist review
        var reviewQuery = await _unitOfWork.ReviewRepository.GetAsync(
            review => review.ReviewerId == currentAccount.Id && review.PostId == request.PostId);
        
        if (reviewQuery.Any())
        {
            throw new ApiException(ResponseCode.ReviewErrorExisted);
        }
        
        var review = new Review()
        {
            ReviewerId = currentAccount.Id,
            AccountId = (long) post.FreelancerId,
            Score = request.Score,
            Content = request.Content,
            PostId = post.Id
        };

        await _unitOfWork.ReviewRepository.AddAsync(review);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ReviewResponse>(review);
    }
}