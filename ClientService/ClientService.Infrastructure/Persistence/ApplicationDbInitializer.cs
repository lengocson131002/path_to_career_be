using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Infrastructure.Persistence;

public class ApplicationDbInitializer
{
    private readonly ILogger<ApplicationDbInitializer> _logger;

    private readonly ApplicationDbContext _context;

    public ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                _logger.LogInformation("Migrating Database");
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while migrating the database: {0}", ex.Message);
            throw;
        }
    }

    public Task SeedAsync()
    {
        return Task.CompletedTask;
    }

}