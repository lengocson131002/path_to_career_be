using FluentValidation;
using IdentityService.Application.Auth.Models;
using MediatR;

namespace IdentityService.Application.Auth.Commands;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(model => model.RefreshToken)
            .NotEmpty();
    }
}

public class RefreshTokenRequest : IRequest<TokenResponse>
{
    public string RefreshToken { get; set; } = default!;
}