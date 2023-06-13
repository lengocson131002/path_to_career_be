using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Notifications.Commands;
using ClientService.Application.Notifications.Models;
using ClientService.Application.Notifications.Queries;
using ClientService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/notifications")]
public class NotificationController : ApiControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginationResponse<Notification, NotificationResponse>>> GetAllNotification(
        [FromQuery] GetAllNotificationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SortColumn))
        {
            request.SortColumn = "CreatedAt";
            request.SortDir = SortDirection.Desc;
        }

        return await Mediator.Send(request);
    }

    [HttpPost]
    public async Task<ActionResult<StatusResponse>> PushNotification([FromBody] PushNotificationRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPut("{id}/read")]
    [Authorize]
    public async Task<ActionResult<NotificationResponse>> ReadNotification([FromRoute] long id)
    {
        var request = new ReadNotificationRequest()
        {
            Id = id
        };
        return await Mediator.Send(request);
    }
    
    [HttpPut("{id}/unread")]
    [Authorize]
    public async Task<ActionResult<NotificationResponse>> UnreadNotification([FromRoute] long id)
    {
        var request = new UnreadNotificationRequest()
        {
            Id = id
        };
        return await Mediator.Send(request);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<NotificationResponse>> RemoveNotification([FromRoute] long id)
    {
        var request = new RemoveNotificationRequest()
        {
            Id = id
        };
        return await Mediator.Send(request);
    }
}