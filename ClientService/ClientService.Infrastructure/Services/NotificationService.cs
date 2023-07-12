using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Persistence;
using ClientService.Noti;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification = ClientService.Domain.Entities.Notification;

namespace ClientService.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<NotificationService> _logger;
    private readonly string _notificationGrpcUrl;

    public NotificationService(
        IUnitOfWork unitOfWork, 
        ILogger<NotificationService> logger,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _notificationGrpcUrl = configuration["Grpc:Url"] ?? throw new ArgumentException("Grpc:Url is required");
    }

    public async Task<bool> PushNotification(Notification notification)
    {
        try
        {
            // store into db
            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            // Sending notification to Notification GRPC Service
            using var channel = GrpcChannel.ForAddress(_notificationGrpcUrl);
            var client = new Noti.Notification.NotificationClient(channel);

            var notificationRequest = new NotificationRequest()
            {
                Id = notification.Id,
                Type = notification.Type.ToString(),
                AccountId = notification.AccountId ?? 0,
                Content = notification.Content,
                Data = notification.Data,
                ReferenceId = notification.ReferenceId,
                ReadAt = notification.ReadAt?.ToUnixTimeMilliseconds() ?? 0, 
                CreatedAt = notification.CreatedAt.ToUnixTimeMilliseconds(),
                UpdatedAt = notification.UpdatedAt.ToUnixTimeMilliseconds(),
                DeletedAt = notification.DeletedAt?.ToUnixTimeMilliseconds() ?? 0,
            };
            
            await client.PushNotificationAsync(notificationRequest);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Push notification error {0}", ex.Message);
        }

        return false;
    }

    public async Task<bool> PushNotificationNotSave(Notification notification)
    {
        try
        {
            // Sending notification to Notification GRPC Service
            using var channel = GrpcChannel.ForAddress(_notificationGrpcUrl);
            var client = new Noti.Notification.NotificationClient(channel);

            var notificationRequest = new NotificationRequest()
            {
                Id = notification.Id,
                Type = notification.Type.ToString(),
                AccountId = notification.AccountId ?? 0,
                Content = notification.Content ?? string.Empty,
                Data = notification.Data ?? string.Empty,
                ReferenceId = notification.ReferenceId ?? string.Empty,
                ReadAt = notification.ReadAt?.ToUnixTimeMilliseconds() ?? 0, 
                CreatedAt = notification.CreatedAt.ToUnixTimeMilliseconds(),
                UpdatedAt = notification.UpdatedAt.ToUnixTimeMilliseconds(),
                DeletedAt = notification.DeletedAt?.ToUnixTimeMilliseconds() ?? 0,
            };
            
            await client.PushNotificationAsync(notificationRequest);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Push notification error {0}", ex.Message);
        }

        return false;
    }
}