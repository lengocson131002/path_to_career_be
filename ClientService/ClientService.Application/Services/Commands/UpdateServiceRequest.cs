using System.Text.Json.Serialization;
using ClientService.Application.Services.Models;
using FluentValidation;
using MediatR;

namespace ClientService.Application.Services.Commands;

public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequest>
{
    public UpdateServiceRequestValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .When(model => model.Name != null);

        RuleFor(model => model.Price)
            .GreaterThan(0)
            .When(model => model.Price != null);

        RuleFor(model => model.Discount)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Discount != null);

        RuleFor(model => model.ApplyCount)
            .GreaterThan(0)
            .When(model => model.ApplyCount != null);

        RuleFor(model => model.Duration)
            .GreaterThan(0)
            .When(model => model.Duration != null);

        RuleFor(model => model.Order)
            .GreaterThan(1)
            .When(model => model.Order != null);
    }
}

public class UpdateServiceRequest : IRequest<ServiceResponse>
{
    [JsonIgnore]
    public long Id { get; set; }
    
    public string? Name { get; set; } = default!;
    
    public decimal? Price { get; set; }
    
    public double? Discount { get; set; }
    
    public int? ApplyCount { get; set; }
    
    public int? Duration { get; set; } // in Months
    
    public bool? OnTop { get; set; }
    
    public int? Order { get; set; }
    
    public bool? Recommended { get; set; }
    
    public string? Description { get; set; }
}