using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using ClientService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IBaseRepository<Account>? _accountRepository;
    private IBaseRepository<Post>? _postRepository;
    private IBaseRepository<PostApplication>? _postApplicationRepository;
    private IBaseRepository<Major>? _majorRepository;
    private IBaseRepository<Review>? _reviewRepository;
    private IBaseRepository<Service>? _serviceRepository;
    private IBaseRepository<AccountService>? _accountServiceRepository;
    private IBaseRepository<Transaction>? _transactionRepository;
    
    private readonly ApplicationDbContext _dbContext;
    private bool _disposed;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IBaseRepository<Account> AccountRepository =>
        _accountRepository ??= new BaseRepository<Account>(_dbContext);

    public IBaseRepository<Post> PostRepository =>
        _postRepository ??= new BaseRepository<Post>(_dbContext);

    public IBaseRepository<PostApplication> PostApplicationRepository =>
        _postApplicationRepository ??= new BaseRepository<PostApplication>(_dbContext);

    public IBaseRepository<Major> MajorRepository => _majorRepository ??= new BaseRepository<Major>(_dbContext);

    public IBaseRepository<Review> ReviewRepository => _reviewRepository ??= new BaseRepository<Review>(_dbContext);

    public IBaseRepository<Service> ServiceRepository => _serviceRepository ??= new BaseRepository<Service>(_dbContext);

    public IBaseRepository<AccountService> AccountServiceRepository =>
        _accountServiceRepository ??= new BaseRepository<AccountService>(_dbContext);

    public IBaseRepository<Transaction> TransactionRepository =>
        _transactionRepository ??= new BaseRepository<Transaction>(_dbContext);

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Rollback()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _dbContext.Dispose();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}