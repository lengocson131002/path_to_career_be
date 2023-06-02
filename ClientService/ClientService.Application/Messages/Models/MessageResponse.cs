namespace ClientService.Application.Messages.Models;

public class MessageResponse
{
    public long Id { get; set; }

    public MessageType Type { get; set; }

    public string Content { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public long AccountId { get; set; }
}