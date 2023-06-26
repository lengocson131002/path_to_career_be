using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Domain.Entities;
using ClientService.Infrastructure.Persistence;

namespace ClientService.Infrastructure.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}