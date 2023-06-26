using ClientService.Application.Dashboard.Models;

namespace ClientService.Application.Dashboard.Queries;

public class GetPostStatisticRequest : IRequest<PostStatisticResponse>
{
    public DateTimeOffset? From { get; set; }
    
    public DateTimeOffset? To { get; set; }
}