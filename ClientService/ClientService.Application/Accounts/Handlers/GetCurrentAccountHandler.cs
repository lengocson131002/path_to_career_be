using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Services.Models;
using ClientService.Domain.Entities;
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
        
        // Count post
        var postQuery = await _unitOfWork.PostRepository.GetAsync(post => post.AccountId == account.Id);
        var postCount = await postQuery.CountAsync(cancellationToken);

        var response = _mapper.Map<AccountDetailResponse>(account);
        response.PostCount = postCount;

        return response;
    }
}