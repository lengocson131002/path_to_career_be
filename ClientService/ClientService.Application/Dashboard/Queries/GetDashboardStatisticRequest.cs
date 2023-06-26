using ClientService.Application.Dashboard.Models;

namespace ClientService.Application.Dashboard.Queries;

public class GetDashboardStatisticRequest : IRequest<StatisticResponse>
{
    public DateTimeOffset? From { get; set; }
    
    public DateTimeOffset? To { get; set; }
}