using ClientService.Application.Accounts.Commands;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Accounts.Handlers;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ILogger<UpdatePasswordHandler> _logger;

    private readonly ICurrentAccountService _currentAccountService;
    
    public UpdatePasswordHandler(
        IUnitOfWork unitOfWork, 
        ILogger<UpdatePasswordHandler> logger,
        ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _currentAccountService = currentAccountService;
    }

    public async Task Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        if (!string.Equals(request.Password, request.ConfirmPassword))
        {
            throw new ApiException(ResponseCode.AccountErrorPasswordNotMatched);
        }

        var account = await _currentAccountService.GetCurrentAccount();

        account.Password = request.Password;
        
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync();
    }
}