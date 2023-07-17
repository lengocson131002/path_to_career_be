using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Domain.Entities;

public class Review : BaseAuditableEntity
{
    public long Id { get; set; }
    
    public long ReviewerId { get; set; }
    
    [ForeignKey("ReviewerId")]
    public Account Reviewer { get; set; } = default!;
    
    public long AccountId { get; set; }
    
    [ForeignKey("AccountId")]
    public Account Account { get; set; } = default!;
    
    public double Score { get; set; }

    public string Content { get; set; } = default!;
    
    public long? PostId { get; set; }
    
    [ForeignKey("PostId")]
    public Post? Post { get; set; } 
}