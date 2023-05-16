using FluentValidation;
using IdentityService.Application.Auth.Models;
using MediatR;

namespace IdentityService.Application.Auth.Commands;

public class LoginWithGoogleRequestValidator : AbstractValidator<LoginWithGoogleRequest>
{
    public LoginWithGoogleRequestValidator()
    {
        RuleFor(model => model.IdToken)
            .NotEmpty();
    }    
}

public class LoginWithGoogleRequest : IRequest<TokenResponse>
{
    public string IdToken { get; set; } = default!;
}