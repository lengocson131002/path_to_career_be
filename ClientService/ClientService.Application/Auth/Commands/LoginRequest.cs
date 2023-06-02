using ClientService.Application.Auth.Models;
using FluentValidation;

namespace ClientService.Application.Auth.Commands;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(model => model.Email)
            .NotEmpty();
        RuleFor(model => model.Password)
            .NotEmpty();
    }
}

public class LoginRequest : IRequest<TokenResponse>
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}