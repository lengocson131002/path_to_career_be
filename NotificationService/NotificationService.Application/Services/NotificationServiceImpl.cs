using System.Text.Json;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Application.SignalR;

namespace NotificationService.Application.Services;

public class NotificationServiceImpl : Notification.NotificationBase
{
    private const string ReceiveNotificationMethod = "ReceiveNotification";
    private readonly ILogger<NotificationServiceImpl> _logger;
    private readonly IHubContext<NotificationHub> _notificationHubContext;

    public NotificationServiceImpl(IHubContext<NotificationHub> notificationHubContext,
        ILogger<NotificationServiceImpl> logger)
    {
        _notificationHubContext = notificationHubContext;
        _logger = logger;
    }

    public override async Task<NotificationResponse> PushNotification(NotificationRequest request,
        ServerCallContext context)
    {
        var signalRNotification = new SignalRNotification
        {
            Id = request.Id,
            AccountId = request.AccountId != 0 ? request.AccountId : null,
            Type = request.Type,
            Content = request.Content,
            Data = !string.IsNullOrWhiteSpace(request.Data) ? request.Data : null,
            ReferenceId = !string.IsNullOrWhiteSpace(request.ReferenceId) ? request.ReferenceId : null,
            ReadAt = request.ReadAt > 0
                ? DateTimeOffset.FromUnixTimeMilliseconds(request.ReadAt)
                : null,
            CreatedAt = request.CreatedAt > 0
                ? DateTimeOffset.FromUnixTimeMilliseconds(request.CreatedAt)
                : null,
            UpdatedAt = request.UpdatedAt > 0
                ? DateTimeOffset.FromUnixTimeMilliseconds(request.UpdatedAt)
                : null,
            DeletedAt = request.DeletedAt > 0
                ? DateTimeOffset.FromUnixTimeMilliseconds(request.DeletedAt)
                : null
        };

        await SendSignalRNotification(signalRNotification);
        _logger.LogInformation("Push notification: {0}", JsonSerializer.Serialize(signalRNotification));
        return new NotificationResponse
        {
            Status = true
        };
    }

    private async Task SendSignalRNotification(SignalRNotification notification)
    {
        var accId = notification.AccountId;
        if (accId != null && accId > 0)
        {
            var connection = new NotificationConnection((long)accId);
            await _notificationHubContext.Clients.Groups(connection.RoomName)
                .SendAsync(ReceiveNotificationMethod, notification);
        }
        else
        {
            await _notificationHubContext.Clients.All.SendAsync(ReceiveNotificationMethod, notification);
        }
    }
}