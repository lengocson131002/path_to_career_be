using ClientService.Application.Common.Models.Response;
using FluentValidation;
using MediatR;

namespace ClientService.Application.Accounts.Commands;

public class RegisterServiceRequestValidation : AbstractValidator<RegisterServiceRequest>
{
    public RegisterServiceRequestValidation ()
    {
        RuleFor(model => model.ServiceId)
            .NotNull();
    }
}
public class RegisterServiceRequest : IRequest<StatusResponse>
{
    public long ServiceId { get; set; }
}