using ClientService.Application.Accounts.Models;

namespace ClientService.Application.Transactions.Models;

public class TransactionResponse
{
    public long Id { get; set; }
    
    public AccountResponse Account { get; set; } = default!;
    
    public string? ReferenceTransactionId { get; set; }
    
    public decimal Amount { get; set; }
    
    public PaymentMethod PayMethod { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
    public long PostId { get; set; }

    public TransactionStatus Status { get; set; }
    
    public string? Content { get; set; }
}