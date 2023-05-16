using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public long Id { get; set; }
    
    public string Username { get; set; }
    
    public string? Password { get; set; }
    
    public string? EmailAddress { get; set; }

    public Role Role { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}