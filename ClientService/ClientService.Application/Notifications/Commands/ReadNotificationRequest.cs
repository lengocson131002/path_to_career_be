using ClientService.Application.Notifications.Models;

namespace ClientService.Application.Notifications.Commands;

public class ReadNotificationRequest : IRequest<NotificationResponse>
{
    public long Id { get; set; }
}