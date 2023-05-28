namespace ClientService.Domain.Entities;

public class AccountService : BaseAuditableEntity
{
    public long Id { get; set; }
    
    public long AccountId { get; set; }

    public Account Account { get; set; } = default!;
    
    public long ServiceId { get; set; }

    public Service Service { get; set; } = default!;

    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset? CancelTime { get; set; }
    
    public int ApplyCount { get; set; }
    
    public long TransactionId { get; set; }

    public Transaction Transaction { get; set; } = default!;
}