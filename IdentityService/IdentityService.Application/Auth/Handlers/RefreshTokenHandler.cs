using System.Security.Claims;
using IdentityService.Application.Auth.Commands;
using IdentityService.Application.Auth.Models;
using IdentityService.Application.Common.Enums;
using IdentityService.Application.Common.Exceptions;
using IdentityService.Application.Common.Interfaces;
using MediatR;

namespace IdentityService.Application.Auth.Handlers;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, TokenResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenHandler(
        ICurrentUserService currentUserService, 
        IJwtService jwtService, 
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    public async Task<TokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var principal = _currentUserService.GetCurrentPrincipalFromToken(request.RefreshToken);
        var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (username == null)
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidRefreshToken);
        }

        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(
            account => account.Username.ToLower().Equals(username.ToLower())
        );

        var account = accountQuery.FirstOrDefault();
        if (account == null)
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidRefreshToken);
        }

        var newAccessToken = _jwtService.GenerateJwtToken(account);
        var newRefreshToken = _jwtService.GenerateJwtRefreshToken(account);

        return new TokenResponse(newAccessToken, newRefreshToken);
    }
}