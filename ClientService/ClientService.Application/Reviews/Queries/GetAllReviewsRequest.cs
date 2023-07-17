using System.Linq.Expressions;
using System.Text.Json.Serialization;
using ClientService.Application.Common.Models.Request;
using ClientService.Application.Reviews.Models;
using ClientService.Domain.Entities;
using LinqKit;

namespace ClientService.Application.Reviews.Queries;

public class GetAllReviewsRequest : PaginationRequest<Review>, IRequest<PaginationResponse<Review, ReviewResponse>>
{
    public long? ReviewerId { get; set; }
    
    public long? AccountId { get; set; }

    private string? _query;
    
    public string? Query
    {
        get => _query;
        set => _query = value?.ToLower().Trim();
    }
    
    public override Expression<Func<Review, bool>> GetExpressions()
    {
        var expression = PredicateBuilder.New<Review>(true);

        if (ReviewerId != null)
        {
            expression = expression.And(review => review.ReviewerId == ReviewerId);
        }
        
        if (AccountId != null)
        {
            expression = expression.And(review => review.AccountId == AccountId);
        }
        
        if (!string.IsNullOrWhiteSpace(Query))
        {
            expression = expression.And(review => review.Content.ToLower().Contains(Query));
        }
        
        return expression;

    }
}