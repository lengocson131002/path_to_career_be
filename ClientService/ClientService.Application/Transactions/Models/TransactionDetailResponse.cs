using ClientService.Application.Accounts.Models;
using ClientService.Application.Posts.Models;

namespace ClientService.Application.Transactions.Models;

public class TransactionDetailResponse
{
    public long Id { get; set; }
    
    public AccountResponse Account { get; set; } = default!;
    
    public string? ReferenceTransactionId { get; set; }
    
    public decimal Amount { get; set; }
    
    public PaymentMethod PayMethod { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }

    public PostResponse Post { get; set; } = default!;
    
    public TransactionStatus Status { get; set; }
}