using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Reviews.Commands;
using ClientService.Application.Reviews.Models;
using ClientService.Domain.Entities;

namespace ClientService.Application.Reviews.Handlers;

public class UpdateReviewHandler : IRequestHandler<UpdateReviewRequest, ReviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public UpdateReviewHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<ReviewResponse> Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
    {
        var reviewQuery = await _unitOfWork.ReviewRepository.GetAsync(r => r.Id == request.Id);
        var currentAccount = await _currentAccountService.GetCurrentAccount();

        var review = reviewQuery.FirstOrDefault();

        if (review == null || review.ReviewerId != currentAccount.Id)
        {
            throw new ApiException(ResponseCode.ReviewErrorNotFound);
        }

        review.Score = request.Score ?? review.Score;
        review.Content = request.Content ?? review.Content;

        await _unitOfWork.ReviewRepository.UpdateAsync(review);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ReviewResponse>(review);
    }
}