namespace ClientService.Domain.Entities;

public class Review : BaseAuditableEntity
{
    public long Id { get; set; }
    
    public long ReviewerId { get; set; }

    public Account Reviewer { get; set; } = default!;
    
    public long AccountId { get; set; }

    public Account Account { get; set; } = default!;
    
    public double Score { get; set; }

    public string Content { get; set; } = default!;
    
}