using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Account> AccountRepository { get; }
    IBaseRepository<Post> PostRepository { get; }
    IBaseRepository<PostApplication> PostApplicationRepository { get; }
        
    IBaseRepository<Major> MajorRepository { get; }
    
    IBaseRepository<Review> ReviewRepository { get; }

    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}