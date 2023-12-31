using System.Linq.Expressions;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Models.Request;
using ClientService.Domain.Entities;
using LinqKit;

namespace ClientService.Application.Accounts.Queries;

public class GetAllAccountRequest : PaginationRequest<Account>, IRequest<PaginationResponse<Account, AccountResponse>>
{
    public Role? Role { get; set; }

    private string? _query;
    
    public string? Query
    {
        get => _query;
        set => _query = value?.ToLower().Trim();
    }
    
    public long[]? MajorIds { get; set; }

    public override Expression<Func<Account, bool>> GetExpressions()
    {
        var expression = PredicateBuilder.New<Account>(true);
        
        if (Role != null)
        {
            expression = expression.And(acc => Role.Equals(acc.Role));
        }

        if (!string.IsNullOrWhiteSpace(Query))
        {
            var queryExpression = PredicateBuilder.New<Account>();
            queryExpression = queryExpression.Or(acc => acc.FullName.ToLower().Contains(Query));
            queryExpression = queryExpression.Or(acc => acc.Email.ToLower().Contains(Query));
            queryExpression = queryExpression.Or(acc => acc.PhoneNumber != null && acc.PhoneNumber.ToLower().Contains(Query));
            expression = expression.And(queryExpression);
        }

        if (MajorIds != null && MajorIds.Length > 0)
        {
            expression = expression.And(acc => acc.Majors.FirstOrDefault(m => MajorIds.Contains(m.Id)) != null);
        }

        return expression;
    }
}