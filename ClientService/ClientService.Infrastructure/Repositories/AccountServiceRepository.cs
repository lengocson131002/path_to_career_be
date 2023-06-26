using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Domain.Entities;
using ClientService.Infrastructure.Persistence;

namespace ClientService.Infrastructure.Repositories;

public class AccountServiceRepository : BaseRepository<AccountService>, IAccountServiceRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public AccountServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}