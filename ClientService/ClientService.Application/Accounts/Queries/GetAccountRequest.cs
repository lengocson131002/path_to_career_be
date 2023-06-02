using ClientService.Application.Accounts.Models;

namespace ClientService.Application.Accounts.Queries;

public class GetAccountRequest : IRequest<AccountDetailResponse>
{
    public long Id { get; private set; }

    public GetAccountRequest(long id)
    {
        Id = id;
    }
}