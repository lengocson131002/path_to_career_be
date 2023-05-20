using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; }
    DbSet<Post> Posts { get; }
}