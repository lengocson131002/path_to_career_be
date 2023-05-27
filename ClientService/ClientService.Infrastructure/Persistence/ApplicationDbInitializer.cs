using System.Linq.Dynamic.Core;
using ClientService.Application.Common.Interfaces;
using ClientService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Infrastructure.Persistence;

public class ApplicationDbInitializer
{
    private readonly ILogger<ApplicationDbInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _context = context;
        _unitOfWork = unitOfWork;
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

    public async Task SeedAsync()
    {
        try
        {
            await _context.Database.EnsureCreatedAsync();
            
            if (!_context.Accounts.Any())
            {
                // Seeding account data
                await _unitOfWork.AccountRepository.AddRange(AccountSeeding.DefaultAccounts);
            }

            if (!_context.Majors.Any())
            {
                await _unitOfWork.MajorRepository.AddRange(MajorSeeding.Majors);
            }
            
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Seeding database error {0}", ex.Message);
        }
    }

}