using ClientService.Application.Auth.Commands;
using ClientService.Application.Auth.Models;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Auth.Handlers;

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
                predicate: account => account.Email.ToLower().Equals(request.Email.ToLower()) 
                                      && account.Password != null 
                                      && account.Password.Equals(request.Password)
            );

        var user = userQuery.FirstOrDefault();
        if (user == null)
        {
            throw new ApiException(ResponseCode.AuthErrorInvalidEmailOrPassword);
        }

        var token = _jwtService.GenerateJwtToken(user);
        var refreshToken = _jwtService.GenerateJwtRefreshToken(user);

        return new TokenResponse(token, refreshToken);
    }
}