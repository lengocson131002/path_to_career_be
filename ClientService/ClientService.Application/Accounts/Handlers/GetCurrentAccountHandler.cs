using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Interfaces;
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
        var account = await _currentAccountService.GetCurrentAccount();
        
        // Load major
        var majorQuery =
            await _unitOfWork.AccountMajorRepository.GetAsync(item => item.AccountId == account.Id);

        account.Majors = majorQuery.Select(item => item.Major).ToList(); 
            
        return _mapper.Map<AccountDetailResponse>(account);
    }
}