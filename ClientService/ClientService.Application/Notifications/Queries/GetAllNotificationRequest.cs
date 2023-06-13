using System.Linq.Expressions;
using ClientService.Application.Common.Models.Request;
using ClientService.Application.Notifications.Models;
using ClientService.Domain.Entities;
using LinqKit;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClientService.Application.Notifications.Queries;

public class GetAllNotificationRequest : PaginationRequest<Notification>,
    IRequest<PaginationResponse<Notification, NotificationResponse>>
{
    public DateTimeOffset? From { get; set; }

    public DateTimeOffset? To { get; set; }

    public bool? Read { get; set; }

    [BindNever] public long? AccountId { get; set; }

    public NotificationType? Type { get; set; }

    public override Expression<Func<Notification, bool>> GetExpressions()
    {
        var expression = PredicateBuilder.New<Notification>(true);

        expression = expression.And(n => From == null || n.CreatedAt >= From);
        expression = expression.And(n => To == null || n.CreatedAt <= To);
        expression = expression.And(n => Read == null
                                         || (Read == true && n.ReadAt != null)
                                         || (Read == false && n.ReadAt == null));
        expression = expression.And(n => AccountId == null || n.AccountId == null || n.AccountId == AccountId);
        
        return expression;
    }
}