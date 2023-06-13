using ClientService.Application.Notifications.Commands;
using ClientService.Domain.Entities;

namespace ClientService.Application.Notifications.Handlers;

public class PushNotificationHandler : IRequestHandler<PushNotificationRequest, StatusResponse>
{
    private readonly INotificationService _notificationService;

    public PushNotificationHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<StatusResponse> Handle(PushNotificationRequest request, CancellationToken cancellationToken)
    {
        var notification = new Notification(NotificationType.MessageCreated)
        {
            AccountId = request.AccountId,
            Data = string.Empty,
            ReferenceId = "1",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await _notificationService.PushNotificationNotSave(notification);
        return new StatusResponse(true);
    }
}