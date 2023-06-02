using ClientService.Application.Greeting.Models;
using FluentValidation;

namespace ClientService.Application.Greeting.Commands;

public class GreetingCommandValidator : AbstractValidator<GreetingRequest> {
    
    public GreetingCommandValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty();
    }
}

public class GreetingRequest : IRequest<GreetingResponse>
{
    public string Name { get; set; }
}