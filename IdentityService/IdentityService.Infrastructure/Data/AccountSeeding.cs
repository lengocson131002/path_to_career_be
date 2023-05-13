using IdentityService.Domain.Entities;

namespace IdentityService.Infrastructure.Data;

public static class AccountSeeding
{
    public static IList<Account> DefaultAccounts => new List<Account>()
    {
        new Account()
        {
            Username = "admin",
            Password = "Aqswde123@",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        }
    };
}