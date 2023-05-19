using ClientService.Application.Accounts.Models;
using MediatR;

namespace ClientService.Application.Accounts.Queries;

public class GetCurrentAccountRequest : IRequest<AccountDetailResponse>
{
    
}