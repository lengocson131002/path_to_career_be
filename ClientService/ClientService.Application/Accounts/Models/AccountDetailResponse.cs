using ClientService.Application.Majors.Models;

namespace ClientService.Application.Accounts.Models;

public class AccountDetailResponse : AccountResponse
{
    public IList<MajorResponse> Majors { get; set; } = default!;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
    
}