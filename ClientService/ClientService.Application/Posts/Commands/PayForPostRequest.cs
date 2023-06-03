using System.Text.Json.Serialization;
using ClientService.Application.Transactions.Models;
using FluentValidation;

namespace ClientService.Application.Posts.Commands;

public class PayForPostRequestValidator : AbstractValidator<PayForPostRequest>
{
    public PayForPostRequestValidator()
    {
        RuleFor(model => model.Method)
            .NotNull();
    }
}
public class PayForPostRequest : IRequest<TransactionResponse>
{
    [JsonIgnore]
    public long PostId { get; set; }
    public PaymentMethod Method { get; set; }
}