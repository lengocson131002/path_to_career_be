using System.ComponentModel.DataAnnotations;
using ClientService.Domain.Enums;

namespace ClientService.Domain.Entities;

public class Transaction
{
    [Key]
    public long Id { get; set; }
    
    public long AccountId { get; set; }

    public Account Account { get; set; } = default!;
    
    public string? ReferenceTransactionId { get; set; }
    
    public decimal Amount { get; set; }
    
    public PaymentMethod PayMethod { get; set; }
    
    public DateTimeOffset PaymentTime { get; set; }
    
}