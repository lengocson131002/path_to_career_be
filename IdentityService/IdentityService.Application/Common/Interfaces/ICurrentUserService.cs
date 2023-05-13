using System.Security.Claims;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public Account? GetCurrentAccount();
    
    public string? CurrentPrincipal { get; }

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token);

}