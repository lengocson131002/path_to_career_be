using ClientService.Domain.Enums;

namespace ClientService.Infrastructure.SignalR;

public class ChatMessage
{
    public MessageType Type { get; set; }

    public string Content { get; set; } = default!;
}