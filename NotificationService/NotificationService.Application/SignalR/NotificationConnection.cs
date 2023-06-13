namespace NotificationService.Application.SignalR;

public class NotificationConnection
{
    public long AccountId { get; set; }

    public NotificationConnection()
    {
    }

    public NotificationConnection(long accountId)
    {
        this.AccountId = accountId;
    }
    
    public string RoomName => $"notification_{AccountId}";
}