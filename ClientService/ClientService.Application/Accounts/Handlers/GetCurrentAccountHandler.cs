using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Services.Models;
using ClientService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Accounts.Handlers;

public class GetCurrentAccountHandler : IRequestHandler<GetCurrentAccountRequest, AccountDetailResponse>
{
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCurrentAccountHandler(ICurrentAccountService currentAccountService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _currentAccountService = currentAccountService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountDetailResponse> Handle(GetCurrentAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _currentAccountService.GetCurrentAccount(new List<Expression<Func<Account, object>>>()
        {
            acc => acc.Majors,
            acc => acc.Reviews
        });

        var response = _mapper.Map<AccountDetailResponse>(account); 

        // Get current registered service
        var registrationQuery = await _unitOfWork.AccountServiceRepository.GetAsync(
            x => x.AccountId == account.Id
                                   && x.StartTime.AddMonths(x.Service.Duration) > DateTimeOffset.UtcNow
                                   && x.CancelTime == null,
            includes: new ListResponse<Expression<Func<AccountService, object>>>()
            {
                reg => reg.Service
            });

        var registration = await registrationQuery.FirstOrDefaultAsync(cancellationToken);
        if (registration != null)
        {
            response.RegisteredService = _mapper.Map<RegistrationResponse>(registration);
        }
        
        return response;
    }
}