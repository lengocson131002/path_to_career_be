using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Accounts.Handlers;

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
                account => account.Email.ToLower().Equals(request.Email.ToLower()));

        var user = userQuery.FirstOrDefault();
        if (user != null)
        {
            throw new ApiException(ResponseCode.AccountErrorEmailExisted);
        }

        var account = _mapper.Map<Account>(request);
        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Create new Account: {0}", account.Email);

        return _mapper.Map<AccountResponse>(account);
    }
}