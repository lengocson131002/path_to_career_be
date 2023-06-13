namespace NotificationService.Application.SignalR;

public class NotificationConnectionManager
{
    public IDictionary<string, NotificationConnection> Connections { get; }

    public NotificationConnectionManager()
    {
        Connections = new Dictionary<string, NotificationConnection>();
    }
}