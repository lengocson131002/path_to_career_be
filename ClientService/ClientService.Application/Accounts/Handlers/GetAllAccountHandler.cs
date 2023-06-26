using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Persistence;
using ClientService.Domain.Entities;

namespace ClientService.Application.Accounts.Handlers;

public class GetAllAccountsHandler : IRequestHandler<GetAllAccountRequest, PaginationResponse<Account, AccountResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;

    public GetAllAccountsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<Account, AccountResponse>> Handle(GetAllAccountRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.AccountRepository.GetAsync(
            predicate: request.GetExpressions(),
            orderBy: request.GetOrder(),
            includes: new List<Expression<Func<Account, object>>>()
            {
                acc => acc.Majors,
                acc => acc.Reviews
            }
        );
        
        return new PaginationResponse<Account, AccountResponse>(
            query, 
            request.PageNumber, 
            request.PageSize, 
            account => _mapper.Map<AccountResponse>(account));
    }
}