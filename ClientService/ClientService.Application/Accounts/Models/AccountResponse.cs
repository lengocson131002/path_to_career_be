using ClientService.Domain.Enums;

namespace ClientService.Application.Accounts.Models;

public class AccountResponse
{
    public long Id { get; set; }

    public string Email { get; set; } = default!;
    
    public string PhoneNumber { get; set; } = default!;

    public Role Role { get; set; }
    
    public string FullName { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public double Score { get; set; }
}