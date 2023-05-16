using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Extensions;
using ClientService.Domain.Enums;
using FluentValidation;
using MediatR;

namespace ClientService.Application.Accounts.Commands;

public class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterAccountRequestValidator()
    {
        RuleFor(model => model.Password)
            .NotEmpty()
            .MinimumLength(8);
        RuleFor(model => model.Role)
            .NotNull();
        RuleFor(model => model.Email)
            .Must(email => email.IsValidEmail())
            .WithMessage("Invalid email address");
    }
}
public class RegisterAccountRequest : IRequest<AccountResponse>
{
    public string Password { get; set; } = default!;
    
    public string Email { get; set; } = default!;

    public Role Role { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}