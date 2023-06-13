using ClientService.Application.Notifications.Models;

namespace ClientService.Application.Notifications.Commands;

public class RemoveNotificationRequest : IRequest<NotificationResponse>
{
    public long Id { get; set; }
}