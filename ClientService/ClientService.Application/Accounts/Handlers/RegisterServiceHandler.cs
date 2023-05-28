using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Models.Response;
using ClientService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Accounts.Handlers;

public class RegisterServiceHandler : IRequestHandler<RegisterServiceRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public RegisterServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<StatusResponse> Handle(RegisterServiceRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        
        // check whether current account has an active registered service
        var serviceQuery = await _unitOfWork.AccountServiceRepository.GetAsync(
            predicate: x => x.AccountId == currentAccount.Id
                            && x.StartTime.AddMonths(x.Service.Duration) < DateTimeOffset.UtcNow
                            && x.CancelTime == null);
        if (serviceQuery.Any())
        {
            throw new ApiException(ResponseCode.ServiceErrorAccountHasCurrentActiveService);
        }

        var service = await _unitOfWork.ServiceRepository.GetByIdAsync(request.ServiceId);
        if (service == null || !service.IsActive)
        {
            throw new ApiException(ResponseCode.ServiceErrorNotFound);
        }

        var registration = new AccountService()
        {
            Account = currentAccount,
            Service = service,
            StartTime = DateTimeOffset.UtcNow
        };

        await _unitOfWork.AccountServiceRepository.AddAsync(registration);
        await _unitOfWork.SaveChangesAsync();

        return new StatusResponse(true);
    }
}