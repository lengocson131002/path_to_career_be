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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostApplication>()
            .HasIndex(e => e.Id)
            .IsUnique();
        modelBuilder.Entity<PostApplication>()
            .HasKey(nameof(PostApplication.PostId), nameof(PostApplication.ApplierId));

        modelBuilder.Entity<Account>()
             .HasMany(account => account.Reviews).WithOne(review => review.Reviewer);
        modelBuilder.Entity<Account>()
             .HasMany(account => account.IsReview).WithOne(review => review.Account);
        ;
    }

    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<PostApplication> PostApplications => Set<PostApplication>();
    
    public DbSet<Major> Majors => Set<Major>();

    public DbSet<Review> Reviews => Set<Review>();
}