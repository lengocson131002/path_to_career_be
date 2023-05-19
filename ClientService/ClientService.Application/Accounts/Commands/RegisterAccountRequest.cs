using System.Collections;
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
            .NotNull()
            .Must(role => Role.User.Equals(role) || Role.Freelancer.Equals(role))
            .WithMessage("Invalid role (User, Freelancer)");

        RuleFor(model => model.Email)
            .NotEmpty()
            .Must(email => email.IsValidEmail())
            .WithMessage("Invalid email address");

        RuleFor(model => model.PhoneNumber)
            .NotEmpty()
            .Must(email => email.IsValidPhoneNumber())
            .WithMessage("Invalid phone number");

        RuleFor(model => model.FullName)
            .NotEmpty();
    }
}

public class RegisterAccountRequest : IRequest<AccountResponse>
{
    private string _email = default!;

    public string Email
    {
        get => _email;
        set => _email = value.ToLower();
    }

    public string Password { get; set; } = default!;

    public Role Role { get; set; }

    public string FullName { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public string? Description { get; set; }
    
    public IList<string> MajorCodes { get; set; }
}