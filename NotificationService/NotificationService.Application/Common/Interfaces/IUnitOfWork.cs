namespace NotificationService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}