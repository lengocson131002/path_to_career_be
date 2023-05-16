using System.Security.Claims;
using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public Account? GetCurrentAccount();
    
    public string? CurrentPrincipal { get; }

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token);

}