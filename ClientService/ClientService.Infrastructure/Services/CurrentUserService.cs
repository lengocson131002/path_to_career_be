using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using ClientService.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClientService.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IConfiguration _configuration;

    public CurrentUserService(IHttpContextAccessor accessor, IConfiguration configuration)
    {
        _accessor = accessor;
        _configuration = configuration;
    }

    public Account? GetCurrentAccount()
    {
        var identity = _accessor?.HttpContext?.User.Identity as ClaimsIdentity;
        if (identity == null || !identity.IsAuthenticated) return null;

        var claims = identity.Claims;

        return new Account
        {
            Email = claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
            FullName = claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value ?? string.Empty,
            Role = Enum.Parse<Role>(claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value)
        };
    }

    public string? CurrentPrincipal => GetCurrentAccount()?.Email;

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token)
    {
        var tokenValidationParams = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ??
                                                                               throw new ArgumentException(
                                                                                   "Jwt:Key is required")))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidRefreshToken);
        }

        return principal;
    }
}