using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Account> AccountRepository { get; }
    IBaseRepository<Post> PostRepository { get; }
    IBaseRepository<PostApplication> PostApplicationRepository { get; }
        
    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}