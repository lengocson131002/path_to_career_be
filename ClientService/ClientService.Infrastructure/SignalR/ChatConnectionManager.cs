namespace ClientService.Infrastructure.SignalR;

public class ChatConnectionManager
{
    public IDictionary<string, UserConnection> Connections { get; }

    public ChatConnectionManager()
    {
        Connections = new Dictionary<string, UserConnection>();
    }
}