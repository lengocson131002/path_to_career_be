using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Account> AccountRepository { get; }
    
    IBaseRepository<Post> PostRepository { get; }
    
    IBaseRepository<PostApplication> PostApplicationRepository { get; }
        
    IBaseRepository<Major> MajorRepository { get; }
    
    IBaseRepository<Review> ReviewRepository { get; }
    
    IBaseRepository<Service> ServiceRepository { get; }

    IBaseRepository<AccountService> AccountServiceRepository { get; }
    
    IBaseRepository<Transaction> TransactionRepository { get; }
    
    IBaseRepository<Message> MessageRepository { get; }

    int SaveChanges();

    Task<int> SaveChangesAsync();

    void Rollback();
}