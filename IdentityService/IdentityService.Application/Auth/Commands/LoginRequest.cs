using FluentValidation;
using IdentityService.Application.Auth.Models;
using MediatR;

namespace IdentityService.Application.Auth.Commands;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(model => model.Username)
            .NotEmpty();
        RuleFor(model => model.Password)
            .NotEmpty();
    }
}

public class LoginRequest : IRequest<TokenResponse>
{
    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;
}