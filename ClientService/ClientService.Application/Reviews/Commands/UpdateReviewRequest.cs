using System.Text.Json.Serialization;
using ClientService.Application.Reviews.Models;
using FluentValidation;
using MediatR;

namespace ClientService.Application.Reviews.Commands;

public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
{
    public UpdateReviewRequestValidator()
    {
        RuleFor(model => model.Score)
            .Must(score => score == null || (score > 0 && score <= 5))
            .WithMessage("Review score must be > 0 and <= 5");
    }
}

public class UpdateReviewRequest : IRequest<ReviewResponse>
{
    [JsonIgnore]
    public long Id { get; set; }
    
    public double? Score { get; set; }

    private string? _content;
    public string? Content
    {
        get => _content;
        set => _content = value?.Trim();
    }
}