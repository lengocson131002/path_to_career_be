using ClientService.Domain.Enums;

namespace ClientService.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public long Id { get; set; }
    
    public string Email { get; set; }
    
    public string? Password { get; set; }

    public Role Role { get; set; }
    
    public string? FullName { get; set; }
}