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
            FullName = "Tran Tien Ngoc",
            PhoneNumber = "1234567890",
            Role = Role.Admin
        },
        new Account()
        {
            Email = "test@gmail.com",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            FullName = "Nguyen Thanh Ha",
            PhoneNumber = "0132448791",
            Role = Role.User
        }
    };
}