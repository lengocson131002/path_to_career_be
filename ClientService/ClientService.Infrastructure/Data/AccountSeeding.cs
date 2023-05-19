using ClientService.Domain.Entities;
using ClientService.Domain.Enums;

namespace ClientService.Infrastructure.Data;

public static class AccountSeeding
{
    public static IList<Account> DefaultAccounts => new List<Account>()
    {
        new Account()
        {
            Email = "admin@gmail.com",
            Password = "Aqswde123@",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Role = Role.Admin
        }
    };
}