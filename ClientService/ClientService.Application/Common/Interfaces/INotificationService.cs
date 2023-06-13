using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface INotificationService
{
    public Task<bool> PushNotification(Notification notification);
    
    public Task<bool> PushNotificationNotSave(Notification notification);
}
