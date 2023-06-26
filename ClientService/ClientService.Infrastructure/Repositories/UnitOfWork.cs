using ClientService.Application.Common.Persistence;
using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Infrastructure.Persistence;

namespace ClientService.Infrastructure.Repositories;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    private IAccountRepository? _accountRepository;
    private IAccountServiceRepository? _accountServiceRepository;
    private IMajorRepository? _majorRepository;
    private IMessageRepository? _messageRepository;
    private INotificationRepository? _notificationRepository;
    private IPostApplicationRepository? _postApplicationRepository;
    private IPostRepository? _postRepository;
    private IReviewRepository? _reviewRepository;
    private IServiceRepository? _serviceRepository;
    private ITransactionRepository? _transactionRepository;

    public UnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IAccountRepository AccountRepository =>
        _accountRepository ??= new AccountRepository(_dbContext);

    public IPostRepository PostRepository => _postRepository ??= new PostRepository(_dbContext);

    public IPostApplicationRepository PostApplicationRepository =>
        _postApplicationRepository ??= new PostApplicationRepository(_dbContext);

    public IMajorRepository MajorRepository => _majorRepository ??= new MajorRepository(_dbContext);

    public IReviewRepository ReviewRepository => _reviewRepository ??= new ReviewRepository(_dbContext);

    public IServiceRepository ServiceRepository => _serviceRepository ??= new ServiceRepository(_dbContext);

    public IAccountServiceRepository AccountServiceRepository =>
        _accountServiceRepository ??= new AccountServiceRepository(_dbContext);

    public ITransactionRepository TransactionRepository =>
        _transactionRepository ??= new TransactionRepository(_dbContext);

    public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_dbContext);

    public INotificationRepository NotificationRepository =>
        _notificationRepository ??= new NotificationRepository(_dbContext);
}