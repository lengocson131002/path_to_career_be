using AutoMapper;
using ClientService.Application.Reviews.Commands;
using ClientService.Application.Reviews.Models;

namespace ClientService.Application.Reviews.Handlers;

public class RemoveReviewHandler : IRequestHandler<RemoveReviewRequest, ReviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public RemoveReviewHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<ReviewResponse> Handle(RemoveReviewRequest request, CancellationToken cancellationToken)
    {   
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        var reviewQuery = await _unitOfWork.ReviewRepository.GetAsync(r => r.Id == request.Id);
        var review = reviewQuery.FirstOrDefault();

        if (review == null || review.ReviewerId != currentAccount.Id)
        {
            throw new ApiException(ResponseCode.ReviewErrorNotFound);
        }

        await _unitOfWork.ReviewRepository.DeleteAsync(review);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ReviewResponse>(review);
    }
}