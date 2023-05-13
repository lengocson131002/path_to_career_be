using FluentValidation;
using IdentityService.Application.Accounts.Models;
using IdentityService.Domain.Enums;
using MediatR;

namespace IdentityService.Application.Accounts.Commands;

public class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterAccountRequestValidator()
    {
        RuleFor(model => model.Username)
            .NotEmpty();
        RuleFor(model => model.Password)
            .NotEmpty()
            .MinimumLength(8);
        RuleFor(model => model.Role)
            .NotNull();
    }
}
public class RegisterAccountRequest : IRequest<AccountResponse>
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string? EmailAddress { get; set; }

    public Role Role { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}