using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    
    private readonly AuditableEntitySaveChangesInterceptor _saveChangesInterceptor;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor saveChangesInterceptor) : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_saveChangesInterceptor);
    }

    public DbSet<Account> Accounts => Set<Account>();
}