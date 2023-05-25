namespace ClientService.Domain.Entities;

public class Major : BaseAuditableEntity
{
    public long Id { get; set; } 
        
    public string Name { get; set; } = default!;

    public string Code { get; set; } = default!;

    public IList<Account> Accounts { get; set; } = default!;
}