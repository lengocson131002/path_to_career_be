namespace ClientService.Infrastructure.SignalR;

public class UserConnection
{
    public long AccountId { get; set; }
    public long PostId { get; set; }
    
    public long? PartnerId { get; set; }
}