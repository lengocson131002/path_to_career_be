using ClientService.Application.Common.Models.Request;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using ClientService.Domain.Enums;
using LinqKit;
using MediatR;
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
        public string? Keyword { get; set; }
        public long[] MajorId { get; set; } 
        public ServiceType? ServiceType { get; set; }
        public override Expression<Func<Post, bool>> GetExpressions()
        {
            var expression = PredicateBuilder.New<Post>(true);

            if (ServiceType != null)
            {
                expression = expression.And(post => post.ServiceType.Equals(ServiceType));
            }

            if (MajorId.Any())
            {
                expression = expression.And(post => MajorId.Contains(post.MajorId));
            }

            if (!string.IsNullOrEmpty(Keyword))
            {
                var queryExpression = PredicateBuilder.New<Post>(true);
                queryExpression = queryExpression.Or(post => post.Content.Contains(Keyword));
                queryExpression = queryExpression.Or(post => post.Title.Contains(Keyword));
                expression = expression.And(queryExpression);

            }

            return expression;
        }
    }
}
