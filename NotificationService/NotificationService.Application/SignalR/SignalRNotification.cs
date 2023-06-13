namespace NotificationService.Application.SignalR;

public class SignalRNotification
{
    public long Id { get; set; }

    public string Type { get; set; } = default!;

    public string Content { get; set; } = default!;
    
    public string? Data { get; set; }
    
    public long? AccountId { get; set; }

    public DateTimeOffset? ReadAt { get; set; }

    public string? ReferenceId { get; set; }
    
    public DateTimeOffset? CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public DateTimeOffset? DeletedAt { get; set; }
    
}
