using IdentityService.Application.Auth.Commands;
using IdentityService.Application.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPost("google-login")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> LoginWithGoogle([FromBody] LoginWithGoogleRequest request)
    {
        return await Mediator.Send(request);
    }
}