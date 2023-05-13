using IdentityService.Application.Accounts.Commands;
using IdentityService.Application.Accounts.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[ApiController]
[Route("/api/v1/accounts")]
public class AccountController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AccountResponse>> RegisterAccount([FromBody] RegisterAccountRequest request)
    {
        return await Mediator.Send(request);
    }
}