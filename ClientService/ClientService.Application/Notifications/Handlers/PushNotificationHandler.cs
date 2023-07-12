using ClientService.Application.Notifications.Commands;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Notifications.Handlers;

public class PushNotificationHandler : IRequestHandler<PushNotificationRequest, StatusResponse>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<PushNotificationHandler> _logger;

    public PushNotificationHandler(INotificationService notificationService, ILogger<PushNotificationHandler> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public Task<StatusResponse> Handle(PushNotificationRequest request, CancellationToken cancellationToken)
    {
        var notification = new Notification()
        {
            Content = request.Content,
            Type = request.Type,
            Data = request.Data,
            ReferenceId = request.ReferenceId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _notificationService.PushNotificationNotSave(notification);

        return Task.FromResult(new StatusResponse(true));
    }
}