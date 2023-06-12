using ClientService.Application.Auth.Commands;
using ClientService.Application.Auth.Models;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Auth.Handlers;

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
        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(acc => acc.Email.ToLower().Equals(payload.Email.ToLower()));

        var account = await accountQuery.FirstOrDefaultAsync(cancellationToken);
        if (account == null)
        {
            account = new Account()
            {
                Email = payload.Email,
                FullName = payload.Name ?? payload.Email,
                Role = Role.User,
                Avatar = payload.Picture
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