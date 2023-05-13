using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.Common.Enums;
using IdentityService.Application.Common.Exceptions;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Services;

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
            Username = claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
            EmailAddress = claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value ?? string.Empty,
            FirstName = claims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value ?? string.Empty,
            LastName = claims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value ?? string.Empty,
            Role = Enum.Parse<Role>(claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value)
        };
    }

    public string? CurrentPrincipal => GetCurrentAccount()?.Username;

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token)
    {
        var tokenValidationParams = new TokenValidationParameters
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