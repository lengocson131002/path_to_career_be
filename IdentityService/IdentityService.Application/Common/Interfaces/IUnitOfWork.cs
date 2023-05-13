using IdentityService.Domain.Entities;

namespace IdentityService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Account> AccountRepository { get; }
        
    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}