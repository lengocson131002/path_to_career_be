using FluentValidation;

namespace ClientService.Application.Notifications.Commands;

public class PushNotificationRequestValidator : AbstractValidator<PushNotificationRequest>
{
    public PushNotificationRequestValidator()
    {
        RuleFor(model => model.Type)
            .NotNull();
        RuleFor(model => model.Content)
            .NotEmpty();
    }
}

public class PushNotificationRequest : IRequest<StatusResponse>
{
    
    public NotificationType Type { get; set; }

    public string Content { get; set; } = default!;
    
    public string? Data { get; set; }
    
    public string? ReferenceId { get; set; }

}