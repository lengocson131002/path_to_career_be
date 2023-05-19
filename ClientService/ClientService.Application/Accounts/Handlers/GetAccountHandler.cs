using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using MediatR;

namespace ClientService.Application.Accounts.Handlers;

public class GetAccountHandler : IRequestHandler<GetAccountRequest, AccountDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AccountDetailResponse> Handle(GetAccountRequest request, CancellationToken cancellationToken)
    {
        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(
            predicate: acc => acc.Id == request.Id,
            includes: new List<Expression<Func<Account, object>>>()
            {
                acc => acc.Majors,
                acc => acc.Reviews
            });
        
        var account = accountQuery.FirstOrDefault();

        if (account == null)
        {
            throw new ApiException(ResponseCode.AccountErrorNotFound);
        }

        return _mapper.Map<AccountDetailResponse>(account);
    }
}