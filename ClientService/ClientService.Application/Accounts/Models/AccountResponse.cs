using ClientService.Application.Majors.Models;

namespace ClientService.Application.Accounts.Models;

public class AccountResponse
{
    public long Id { get; set; }
    
    public string? Avatar { get; set; }

    public string Email { get; set; } = default!;
    
    public string PhoneNumber { get; set; } = default!;

    public Role Role { get; set; }
    
    public string FullName { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public double Score { get; set; }
    
    public IList<MajorResponse> Majors { get; set; } = default!;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
}