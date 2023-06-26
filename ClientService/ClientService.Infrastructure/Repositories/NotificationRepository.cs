using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Domain.Entities;
using ClientService.Infrastructure.Persistence;

namespace ClientService.Infrastructure.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public NotificationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}