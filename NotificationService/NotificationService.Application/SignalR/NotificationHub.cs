using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Application.SignalR;

public class NotificationHub : Hub
{
    private readonly NotificationConnectionManager _notificationConnections;
    
    public NotificationHub(NotificationConnectionManager notificationConnections)
    {
        _notificationConnections = notificationConnections;
    }

    public async Task JoinRoom(NotificationConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.RoomName);
        _notificationConnections.Connections[Context.ConnectionId] = connection;
    }

    public async Task Leave()
    {
        if (_notificationConnections.Connections.TryGetValue(Context.ConnectionId,
                out NotificationConnection connection))
        {
            _notificationConnections.Connections.Remove(Context.ConnectionId);
        }

        if (connection != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.RoomName);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_notificationConnections.Connections.TryGetValue(Context.ConnectionId,
                out NotificationConnection connection))
        {
            _notificationConnections.Connections.Remove(Context.ConnectionId);
        }

        return base.OnDisconnectedAsync(exception);
    }
}