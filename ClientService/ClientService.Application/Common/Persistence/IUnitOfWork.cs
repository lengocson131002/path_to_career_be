using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Persistence;

public interface IUnitOfWork : IBaseUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    
    IPostRepository PostRepository { get; }
    
    IPostApplicationRepository PostApplicationRepository { get; }
        
    IMajorRepository MajorRepository { get; }
    
    IReviewRepository ReviewRepository { get; }
    
    IServiceRepository ServiceRepository { get; }

    IAccountServiceRepository AccountServiceRepository { get; }
    
    ITransactionRepository TransactionRepository { get; }
    
    IMessageRepository MessageRepository { get; }
    
    INotificationRepository NotificationRepository { get; }
}