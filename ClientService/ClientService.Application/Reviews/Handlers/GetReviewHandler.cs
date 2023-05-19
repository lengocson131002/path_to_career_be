using AutoMapper;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Reviews.Models;
using ClientService.Application.Reviews.Queries;
using MediatR;

namespace ClientService.Application.Reviews.Handlers;

public class GetReviewHandler : IRequestHandler<GetReviewRequest, ReviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReviewHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReviewResponse> Handle(GetReviewRequest request, CancellationToken cancellationToken)
    {
        var reviewQuery = await _unitOfWork.ReviewRepository.GetAsync(r => r.Id == request.Id);
        var review = reviewQuery.FirstOrDefault();
        if (review == null)
        {
            throw new ApiException(ResponseCode.ReviewErrorNotFound);
        }

        return _mapper.Map<ReviewResponse>(review);
    }
}