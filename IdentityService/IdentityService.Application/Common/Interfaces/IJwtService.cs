using IdentityService.Domain.Entities;

namespace IdentityService.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(Account account);
    
    string GenerateJwtRefreshToken(Account account);

}