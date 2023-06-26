using ClientService.Application.Accounts.Models;

namespace ClientService.Application.Dashboard.Queries;

public class GetTopFreelancerRequest : IRequest<ListResponse<AccountResponse>>
{
    public int? Top { get; set; }
}