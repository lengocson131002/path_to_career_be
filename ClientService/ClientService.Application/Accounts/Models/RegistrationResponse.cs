using ClientService.Application.Services.Models;

namespace ClientService.Application.Accounts.Models;

public class RegistrationResponse
{
    public long Id { get; set; }

    public long AccountId { get; set; }

    public ServiceResponse Service { get; set; } = default!;

    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset? CancelTime { get; set; }
    
    public int ApplyCount { get; set; }
    
    public long TransactionId { get; set; }
}