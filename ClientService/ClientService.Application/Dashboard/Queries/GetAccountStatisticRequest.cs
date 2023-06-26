using ClientService.Application.Dashboard.Models;

namespace ClientService.Application.Dashboard.Queries;

public class GetAccountStatisticRequest : IRequest<AccountStatisticResponse>
{
    public DateTimeOffset? From { get; set; }
    
    public DateTimeOffset? To { get; set; }
}