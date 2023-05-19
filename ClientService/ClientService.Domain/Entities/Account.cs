using System.Collections;
using ClientService.Domain.Enums;

namespace ClientService.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public long Id { get; set; }

    public string Email { get; set; } = default!;
    
    public string PhoneNumber { get; set; } = default!;
    
    public string? Password { get; set; }

    public Role Role { get; set; }

    public string FullName { get; set; } = default!;
    
    public string? Description { get; set; }

    public IList<Major> Majors { get; set; } = default!;

    public IList<Review> Reviews { get; set; } = default!;
}