using ClientService.Application.Services.Models;
using FluentValidation;

namespace ClientService.Application.Services.Commands;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty();

        RuleFor(model => model.Price)
            .GreaterThan(0);
        
        RuleFor(model => model.Discount)
            .GreaterThanOrEqualTo(0);

        RuleFor(model => model.ApplyCount)
            .GreaterThan(0);

        RuleFor(model => model.Duration)
            .GreaterThan(0);

        RuleFor(model => model.OnTop)
            .NotNull();

        RuleFor(model => model.Order)
            .GreaterThan(0);

        RuleFor(model => model.Recommended)
            .NotNull();
    }
}

public class CreateServiceRequest : IRequest<ServiceResponse>
{
    public string Name { get; set; } = default!;
    
    public decimal Price { get; set; }
    
    public double Discount { get; set; }
    
    public int ApplyCount { get; set; }
    
    public int Duration { get; set; } // in Months
    
    public bool OnTop { get; set; }
    
    public int Order { get; set; }
    
    public bool Recommended { get; set; }
    
    public string? Description { get; set; }
}