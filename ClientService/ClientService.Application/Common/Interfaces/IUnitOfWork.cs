namespace ClientService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}