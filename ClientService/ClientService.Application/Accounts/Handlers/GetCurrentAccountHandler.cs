using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using MediatR;

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

        return _mapper.Map<AccountDetailResponse>(account);
    }
}