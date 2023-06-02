using ClientService.Application.Majors.Models;
using ClientService.Application.Services.Models;

namespace ClientService.Application.Accounts.Models;

public class AccountDetailResponse : AccountResponse
{
    public RegistrationResponse? RegisteredService { get; set; }
    
}