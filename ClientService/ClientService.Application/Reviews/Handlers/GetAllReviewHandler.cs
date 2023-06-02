using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Reviews.Models;
using ClientService.Application.Reviews.Queries;
using ClientService.Domain.Entities;

namespace ClientService.Application.Reviews.Handlers;

public class GetAllReviewHandler : IRequestHandler<GetAllReviewsRequest, PaginationResponse<Review, ReviewResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllReviewHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<Review, ReviewResponse>> Handle(GetAllReviewsRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.ReviewRepository.GetAsync(
            predicate: request.GetExpressions(),
            includes: new List<Expression<Func<Review, object>>>()
            {
                review => review.Reviewer,
                review => review.Account
            });

        return new PaginationResponse<Review, ReviewResponse>(
            query, 
            request.PageNumber, 
            request.PageSize, 
            review => _mapper.Map<ReviewResponse>(review));
        
    }
}