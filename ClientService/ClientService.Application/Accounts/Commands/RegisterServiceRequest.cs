using ClientService.Application.Common.Models.Response;
using ClientService.Domain.Enums;
using FluentValidation;
using MediatR;

namespace ClientService.Application.Accounts.Commands;

public class RegisterServiceRequestValidation : AbstractValidator<RegisterServiceRequest>
{
    public RegisterServiceRequestValidation ()
    {
        RuleFor(model => model.ServiceId)
            .NotNull();
        RuleFor(model => model.PaymentMethod)
            .NotNull();
    }
}
public class RegisterServiceRequest : IRequest<StatusResponse>
{
    public long ServiceId { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
}