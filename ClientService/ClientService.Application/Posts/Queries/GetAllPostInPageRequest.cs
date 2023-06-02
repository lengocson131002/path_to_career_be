using ClientService.Application.Common.Models.Request;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Queries
{
    public class GetAllPostInPageRequest : PaginationRequest<Post>, IRequest<PaginationResponse<Post, PostResponse>>
    {
        public long? AccountId { get; set; }
        public string? Keyword { get; set; }
        public long[]? MajorIds { get; set; } 
        public ServiceType? ServiceType { get; set; }
        
        public override Expression<Func<Post, bool>> GetExpressions()
        {
            var expression = PredicateBuilder.New<Post>(true);

            if (ServiceType != null)
            {
                expression = expression.And(post => post.ServiceType.Equals(ServiceType));
            }

            if (MajorIds != null && MajorIds.Any())
            {
                expression = expression.And(post => MajorIds.Contains(post.MajorId));
            }

            if (!string.IsNullOrEmpty(Keyword))
            {
                var queryExpression = PredicateBuilder.New<Post>(true);
                queryExpression = queryExpression.Or(post => post.Content.Contains(Keyword));
                queryExpression = queryExpression.Or(post => post.Title.Contains(Keyword));
                expression = expression.And(queryExpression);

            }

            if (AccountId != null)
            {
                expression = expression.And(post => post.AccountId == AccountId);
            }

            return expression;
        }
    }
}
