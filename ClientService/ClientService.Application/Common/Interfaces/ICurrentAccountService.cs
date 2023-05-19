using System.Linq.Expressions;
using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Interfaces;

public interface ICurrentAccountService
{
    Task<Account> GetCurrentAccount(List<Expression<Func<Account, object>>> includes = null);
}