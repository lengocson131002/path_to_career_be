using FluentValidation;

namespace ClientService.Application.Accounts.Commands;

public class UpdatePasswordRequestValidation : AbstractValidator<UpdatePasswordRequest>
{
    public UpdatePasswordRequestValidation()
    {
        RuleFor(model => model.Password)
            .NotEmpty()
            .MinimumLength(8);
        RuleFor(model => model.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(8);
    }
}

public class UpdatePasswordRequest : IRequest
{
    public string Password { get; set; } = default!;
    
    public string ConfirmPassword { get; set; } = default!;
}