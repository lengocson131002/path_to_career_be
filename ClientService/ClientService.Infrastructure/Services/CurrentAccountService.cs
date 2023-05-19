using System.Linq.Expressions;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;

namespace ClientService.Infrastructure.Services;

public class CurrentAccountService : ICurrentAccountService
{
    private readonly ICurrentUserService _currentPrincipalService;
    private readonly IUnitOfWork _unitOfWork;
    
    public CurrentAccountService(ICurrentUserService currentPrincipalService, IUnitOfWork unitOfWork)
    {
        _currentPrincipalService = currentPrincipalService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Account> GetCurrentAccount(List<Expression<Func<Account, object>>> includes = null)
    {
        var currentPrincipal = _currentPrincipalService.CurrentPrincipal;        
        if (currentPrincipal == null)
        {
            throw new ApiException(ResponseCode.Unauthorized);
        }
        
        var accountQuery =
            await _unitOfWork.AccountRepository.GetAsync(
                predicate: acc => acc.Email.ToLower().Equals(currentPrincipal.ToLower()),
                includes: includes);

        var account = accountQuery.FirstOrDefault();

        return account ?? throw new ApiException(ResponseCode.Unauthorized);
    }
}