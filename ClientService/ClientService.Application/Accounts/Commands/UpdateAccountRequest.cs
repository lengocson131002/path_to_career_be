using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Extensions;
using FluentValidation;

namespace ClientService.Application.Accounts.Commands;

public class UpdateAccountRequestValidator : AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountRequestValidator()
    {
        RuleFor(model => model.PhoneNumber)
            .Must(phone => phone == null || phone.IsValidPhoneNumber())
            .WithMessage("Invalid phone number");

        RuleFor(model => model.MajorCodes)
            .Must(majorCodes => majorCodes == null || majorCodes.Any())
            .WithMessage("Major codes must not be empty");
    }
}

public class UpdateAccountRequest : IRequest<AccountResponse>
{
    public string? Avatar { get; set; }
    
    public string? FullName { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? Description { get; set; }
    
    public IList<string>? MajorCodes { get; set; }
}