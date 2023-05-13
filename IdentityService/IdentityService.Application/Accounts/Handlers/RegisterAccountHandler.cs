using AutoMapper;
using IdentityService.Application.Accounts.Commands;
using IdentityService.Application.Accounts.Models;
using IdentityService.Application.Common.Enums;
using IdentityService.Application.Common.Exceptions;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityService.Application.Accounts.Handlers;

public class RegisterAccountHandler : IRequestHandler<RegisterAccountRequest, AccountResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterAccountHandler> _logger;
    private readonly IMapper _mapper;

    public RegisterAccountHandler(IUnitOfWork unitOfWork, ILogger<RegisterAccountHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AccountResponse> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        var userQuery = await _unitOfWork.AccountRepository.GetAsync(
                account => account.Username.ToLower().Equals(request.Username.ToLower()));

        var user = userQuery.FirstOrDefault();
        if (user != null)
        {
            throw new ApiException(ResponseCode.AccountErrorUsernameExisted);
        }

        var account = _mapper.Map<Account>(request);
        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Create new Account: {0}", account.Username);

        return _mapper.Map<AccountResponse>(account);
    }
}