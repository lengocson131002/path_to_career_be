using System.Linq.Expressions;
using ClientService.Application.Common.Models.Request;
using ClientService.Application.Transactions.Models;
using ClientService.Domain.Entities;
using LinqKit;

namespace ClientService.Application.Transactions.Queries;

public class GetAllTransactionRequest : PaginationRequest<Transaction>, IRequest<PaginationResponse<Transaction, TransactionResponse>>
{
    public long? AccountId { get; set; }

    public string? Query { get; set; }
    public TransactionStatus? Status { get; set; }
    
    public DateTimeOffset? From { get; set; }
    
    public DateTimeOffset? To { get; set; }
        
    public override Expression<Func<Transaction, bool>> GetExpressions()
    {
        var expression = PredicateBuilder.New<Transaction>(true);

        if (!string.IsNullOrEmpty(Query))
        {
            var queryExpression = PredicateBuilder.New<Transaction>(true);
            queryExpression = queryExpression.Or(transaction => transaction.Content.ToLower().Contains(Query.ToLower()));
            expression = expression.And(queryExpression);
        }

        if (AccountId != null)
        {
            expression = expression.And(transaction => transaction.AccountId == AccountId);
        }
            
        if (Status != null)
        {
            expression = expression.And(transaction => Status.Equals(transaction.Status));
        }

        if (From != null)
        {
            expression = expression.And(transaction => transaction.CreatedAt >= From);
        }

        if (To != null)
        {
            expression = expression.And(transaction => transaction.CreatedAt <= To);
        }

        return expression;
    }
}