using IdentityService.Application.Auth.Commands;
using IdentityService.Application.Auth.Models;
using IdentityService.Application.Common.Enums;
using IdentityService.Application.Common.Exceptions;
using IdentityService.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityService.Application.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginRequest, TokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(IUnitOfWork unitOfWork, ILogger<LoginHandler> logger, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _jwtService = jwtService;
    }

    public async Task<TokenResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var userQuery = await _unitOfWork.AccountRepository.GetAsync(
                predicate: account => account.Username.ToLower().Equals(request.Username.ToLower()) && account.Password.Equals(request.Password)
            );

        var user = userQuery.FirstOrDefault();
        if (user == null)
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidUserOrPassword);
        }

        var token = _jwtService.GenerateJwtToken(user);
        var refreshToken = _jwtService.GenerateJwtRefreshToken(user);

        return new TokenResponse(token, refreshToken);
    }
}