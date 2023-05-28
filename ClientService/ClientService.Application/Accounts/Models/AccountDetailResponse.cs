using ClientService.Application.Majors.Models;
using ClientService.Application.Services.Models;

namespace ClientService.Application.Accounts.Models;

public class AccountDetailResponse : AccountResponse
{
    public IList<MajorResponse> Majors { get; set; } = default!;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
    
    public ServiceResponse? Service { get; set; }
}