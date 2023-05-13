using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly string _jwtKey;

    private readonly int _tokenExpireInMinutes;            

    private readonly int _refreshTokenExpireInMinutes;

    private readonly string? _issuer;

    private readonly string? _audience;

    public JwtService(IConfiguration configuration)
    {
        _jwtKey = configuration["Jwt:Key"] ?? throw new ArgumentException("Jwt:Key is required");
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];

        var tokenExpireInMinutes = configuration["Jwt:TokenExpire"];
        _tokenExpireInMinutes = tokenExpireInMinutes != null ? int.Parse(tokenExpireInMinutes) : 5;
        
        var refreshTokenExpireInMinutes = configuration["Jwt:RefreshTokenExpire"];
        _refreshTokenExpireInMinutes = refreshTokenExpireInMinutes != null ? int.Parse(refreshTokenExpireInMinutes) : 30;
    }

    private string GenerateJwtToken(Account account, int expireInMinutes)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Username),
            new Claim(ClaimTypes.Email, account.EmailAddress ?? string.Empty),
            new Claim(ClaimTypes.GivenName, account.FirstName ?? string.Empty),
            new Claim(ClaimTypes.Surname, account.LastName ?? string.Empty),
            new Claim(ClaimTypes.Role, account.Role.ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expireInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateJwtToken(Account account)
    {
        return GenerateJwtToken(account, _tokenExpireInMinutes);
    }

    public string GenerateJwtRefreshToken(Account account)
    {
        return GenerateJwtToken(account, _refreshTokenExpireInMinutes);
    }
}