using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(Account account);
    
    string GenerateJwtRefreshToken(Account account);

}