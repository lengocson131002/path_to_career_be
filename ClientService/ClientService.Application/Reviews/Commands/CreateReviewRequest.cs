using ClientService.Application.Reviews.Models;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace ClientService.Application.Reviews.Commands;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(model => model.AccountId)
            .NotNull();
        
        RuleFor(model => model.Score)
            .Must(score => score > 0 && score <= 5)
            .WithMessage("Review score must be > 0 and <= 5");

        RuleFor(model => model.Content)
            .NotEmpty();
    }
}

public class CreateReviewRequest : IRequest<ReviewResponse>
{
    [JsonIgnore]
    public long AccountId { get; set; }
    
    public double Score { get; set; }
    
    private string _content = default!;
    public string Content
    {
        get => _content;
        set => _content = value.Trim();
    }
}