using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/accounts")]
public class AccountController : ApiControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<AccountDetailResponse>> GetCurrentAccount()
    {
        return await Mediator.Send(new GetCurrentAccountRequest());
    }
    
    [HttpPost]
    public async Task<ActionResult<AccountResponse>> RegisterAccount([FromBody] RegisterAccountRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<AccountResponse>> UpdateAccount([FromBody] UpdateAccountRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPut("password")]
    [Authorize]
    public async Task<ActionResult<StatusResponse>> UpdatePassword([FromBody] UpdatePasswordRequest request)
    {
        await Mediator.Send(request);
        return new StatusResponse(true);
    }
}