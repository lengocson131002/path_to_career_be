using ClientService.Application.Reviews.Models;

namespace ClientService.Application.Reviews.Commands;

public class RemoveReviewRequest : IRequest<ReviewResponse>
{
    public long Id { get; set; }
}