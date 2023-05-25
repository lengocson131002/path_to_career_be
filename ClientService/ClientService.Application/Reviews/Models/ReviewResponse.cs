using ClientService.Application.Accounts.Models;

namespace ClientService.Application.Reviews.Models;

public class ReviewResponse
{
    public long Id { get; set; }

    public AccountResponse Reviewer { get; set; } = default!;

    public AccountResponse Account { get; set; } = default!;

    public double Score { get; set; }

    public string Content { get; set; } = default!;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
}