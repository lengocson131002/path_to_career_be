using ClientService.Application.Accounts.Models;
using MediatR;

namespace ClientService.Application.Accounts.Queries;

public class GetAccountRequest : IRequest<AccountDetailResponse>
{
    public long Id { get; private set; }

    public GetAccountRequest(long id)
    {
        Id = id;
    }
}