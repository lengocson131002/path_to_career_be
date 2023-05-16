using IdentityService.Application.Auth.Commands;
using IdentityService.Application.Auth.Models;
using IdentityService.Application.Common.Enums;
using IdentityService.Application.Common.Exceptions;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityService.Application.Auth.Handlers;

public class LoginWithGoogleHandler : IRequestHandler<LoginWithGoogleRequest, TokenResponse>
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IJwtService _jwtService;
    private readonly ILogger<LoginWithGoogleHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public LoginWithGoogleHandler(
        IJwtService jwtService,
        IGoogleAuthService googleAuthService,
        ILogger<LoginWithGoogleHandler> logger, IUnitOfWork unitOfWork)
    {
        _jwtService = jwtService;
        _googleAuthService = googleAuthService;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<TokenResponse> Handle(LoginWithGoogleRequest request, CancellationToken cancellationToken)
    {
        var payload = await _googleAuthService.VerifyGoogleIdToken(request.IdToken);
        if (payload == null || payload.Email == null)
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidGoogleIdToken);
        }

        // Check if user existed
        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(acc =>
            acc.EmailAddress != null && acc.EmailAddress.ToLower().Equals(payload.Email.ToLower()));

        var account = accountQuery.FirstOrDefault();
        if (account == null)
        {
            account = new Account()
            {
                Username = payload.Email,
                EmailAddress = payload.Email,
                FirstName = payload.Name ?? payload.Email,
                Role = Role.USER
            };
            await _unitOfWork.AccountRepository.AddAsync(account);
        }
        
        // Generate jwt token 
        var accessToken = _jwtService.GenerateJwtToken(account);
        var refreshToken = _jwtService.GenerateJwtRefreshToken(account);
        
        // Save changes
        await _unitOfWork.SaveChangesAsync();

        return new TokenResponse(accessToken, refreshToken);
    }
}