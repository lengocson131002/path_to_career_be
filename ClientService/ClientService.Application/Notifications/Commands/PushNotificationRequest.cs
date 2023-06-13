namespace ClientService.Application.Notifications.Commands;

public class PushNotificationRequest : IRequest<StatusResponse>
{
    public long? AccountId { get; set; }
}