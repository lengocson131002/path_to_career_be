using ClientService.Application.Common.Models.Response;
using ClientService.Application.Services.Commands;
using ClientService.Application.Services.Models;
using ClientService.Application.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/services")]
public class ServiceController : ApiControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> CreateService([FromBody] CreateServiceRequest request)
    {
        return await Mediator.Send(request);
    }
    
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:long}")]
    public async Task<ActionResult<ServiceResponse>> UpdateService([FromRoute] long id, [FromBody] UpdateServiceRequest request)
    {
        request.Id = id;
        return await Mediator.Send(request);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ServiceResponse>> RemoveService([FromRoute] long id)
    {
        var request = new RemoveServiceRequest
        {
            Id = id
        };
        return await Mediator.Send(request);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ListResponse<ServiceResponse>>> GetAllServices()
    {
        var request = new GetAllServicesRequest();
        return await Mediator.Send(request);
    }
    
    [Authorize]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ServiceDetailResponse>> GetServiceDetail([FromRoute] long id)
    {
        var request = new GetServiceDetailRequest
        {
            Id = id
        };
        
        return await Mediator.Send(request);
    }
    
}