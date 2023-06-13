using ClientService.Application.Notifications.Models;

namespace ClientService.Application.Notifications.Commands;

public class UnreadNotificationRequest : IRequest<NotificationResponse>
{
    public long Id { get; set; }
}