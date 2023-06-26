using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Domain.Entities;
using ClientService.Infrastructure.Persistence;

namespace ClientService.Infrastructure.Repositories;

public class MajorRepository : BaseRepository<Major>, IMajorRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public MajorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}