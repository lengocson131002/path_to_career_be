using ClientService.Application.Auth.Models;
using ClientService.Domain.Enums;
using FluentValidation;
using MediatR;

namespace ClientService.Application.Auth.Commands;

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