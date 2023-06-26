using System.Security.Claims;
using ClientService.Application.Auth.Commands;
using ClientService.Application.Auth.Models;
using ClientService.Application.Common.Persistence;

namespace ClientService.Application.Auth.Handlers;

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
        var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (email == null)
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidRefreshToken);
        }

        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(
            account => account.Email.ToLower().Equals(email.ToLower())
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