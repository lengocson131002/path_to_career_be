using System.Security.Claims;

namespace ClientService.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string? CurrentPrincipal { get; }

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token);

}