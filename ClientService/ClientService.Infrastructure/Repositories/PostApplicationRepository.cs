using ClientService.Application.Common.Persistence.Repositories;
using ClientService.Domain.Entities;
using ClientService.Infrastructure.Persistence;

namespace ClientService.Infrastructure.Repositories;

public class PostApplicationRepository : BaseRepository<PostApplication>, IPostApplicationRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public PostApplicationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}