using System.ComponentModel.DataAnnotations;

namespace ClientService.Domain.Entities;

public class Service : BaseAuditableEntity
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = default!;
    
    public decimal Price { get; set; }
    
    public double Discount { get; set; }
    
    public int ApplyCount { get; set; }
    
    public int Duration { get; set; } // in Months
    
    public bool OnTop { get; set; }
    
    public int Order { get; set; }
    
    public bool Recommended { get; set; }
    
    public string? Description { get; set; }

    public bool IsActive => this.DeletedAt == null;
}