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