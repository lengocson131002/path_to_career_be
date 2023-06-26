using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Dashboard.Models;
using ClientService.Application.Dashboard.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/dashboard")]
[Authorize(Roles = "Admin")]
public class DashboardController : ApiControllerBase
{
    [HttpGet("statistics")]
    public async Task<ActionResult<StatisticResponse>> GetStatistic([FromQuery] GetDashboardStatisticRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpGet("accounts")]
    public async Task<ActionResult<AccountStatisticResponse>> GetAccountStatistic([FromQuery] GetAccountStatisticRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpGet("posts")]
    public async Task<ActionResult<PostStatisticResponse>> GetPostStatistic([FromQuery] GetPostStatisticRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpGet("freelancers/top")]
    public async Task<ActionResult<ListResponse<AccountResponse>>> GetTopFreelancers([FromQuery] GetTopFreelancerRequest request)
    {
        return await Mediator.Send(request);
    }
}