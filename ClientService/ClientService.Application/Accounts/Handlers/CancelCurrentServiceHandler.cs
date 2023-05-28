using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Accounts.Handlers;

public class CancelCurrentServiceHandler : IRequestHandler<CancelCurrentServiceRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public CancelCurrentServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<StatusResponse> Handle(CancelCurrentServiceRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        // check whether current account has an active registered service
        var registrationQuery = await _unitOfWork.AccountServiceRepository.GetAsync(
            predicate: x => x.AccountId == currentAccount.Id
                            && x.StartTime.AddMonths(x.Service.Duration) > DateTimeOffset.UtcNow
                            && x.CancelTime == null);
        var currentRegistration = await registrationQuery.FirstOrDefaultAsync(cancellationToken);
        if (currentRegistration == null)
        {
            throw new ApiException(ResponseCode.ServiceErrorAccountHasNoActiveService);
        }
        
        currentRegistration.CancelTime = DateTimeOffset.UtcNow;
        await _unitOfWork.AccountServiceRepository.UpdateAsync(currentRegistration);
        await _unitOfWork.SaveChangesAsync();

        return new StatusResponse(true);
    }
}