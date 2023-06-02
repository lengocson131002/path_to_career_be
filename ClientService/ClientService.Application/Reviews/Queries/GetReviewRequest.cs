using ClientService.Application.Reviews.Models;

namespace ClientService.Application.Reviews.Queries;

public class GetReviewRequest : IRequest<ReviewResponse>
{
    public long Id { get; set; }
}