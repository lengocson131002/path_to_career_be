using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Transactions.Models;
using ClientService.Application.Transactions.Queries;
using ClientService.Domain.Entities;

namespace ClientService.Application.Transactions.Handlers;

public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionRequest, PaginationResponse<Transaction, TransactionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;


    public GetAllTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<PaginationResponse<Transaction, TransactionResponse>> Handle(GetAllTransactionRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        if (!Role.Admin.Equals(currentAccount.Role) && request.AccountId != null && request.AccountId != currentAccount.Id)
        {
            throw new ApiException(ResponseCode.InvalidQuery);
        }

        var query = await _unitOfWork.TransactionRepository.GetAsync(
            predicate: request.GetExpressions(),
            orderBy: request.GetOrder(),
            includes: new ListResponse<Expression<Func<Transaction, object>>>()
            {
                transaction => transaction.Account,
                transaction => transaction.Post
            });

        return new PaginationResponse<Transaction, TransactionResponse>(
            query,
            request.PageNumber, 
            request.PageSize,
            transaction => _mapper.Map<TransactionResponse>(transaction));
    }
}