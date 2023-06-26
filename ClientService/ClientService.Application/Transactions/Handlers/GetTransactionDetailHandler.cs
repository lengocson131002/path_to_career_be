using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Transactions.Models;
using ClientService.Application.Transactions.Queries;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Transactions.Handlers;

public class GetTransactionDetailHandler : IRequestHandler<GetTransactionDetailRequest, TransactionDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTransactionDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TransactionDetailResponse> Handle(GetTransactionDetailRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.TransactionRepository.GetAsync(
            predicate: transaction => transaction.Id == request.Id,
            includes: new ListResponse<Expression<Func<Transaction, object>>>()
            {
                transaction => transaction.Account,
                transaction => transaction.Post
            });

        var transaction = await query.FirstOrDefaultAsync(cancellationToken);
        if (transaction == null)
        {
            throw new ApiException(ResponseCode.TransactionNotFound);
        }

        return _mapper.Map<TransactionDetailResponse>(transaction);
    }

}