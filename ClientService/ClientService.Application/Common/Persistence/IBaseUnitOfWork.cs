namespace ClientService.Application.Common.Persistence;

public interface IBaseUnitOfWork
{
    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}