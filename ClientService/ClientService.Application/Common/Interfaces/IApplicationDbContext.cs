using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; }

    DbSet<Major> Majors { get; }
    
    DbSet<Review> Reviews { get; }
    
    DbSet<Post> Posts { get; }
    
    DbSet<PostApplication> PostApplications { get; }
    
    DbSet<Service> Services { get; }
    
    DbSet<AccountService> AccountServices { get; }

    DbSet<Transaction> Transactions { get; }
    
    DbSet<Message> Messages { get; }

}