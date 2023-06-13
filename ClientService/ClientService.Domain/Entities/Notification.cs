using System.ComponentModel.DataAnnotations;
using ClientService.Domain.Common;
using ClientService.Domain.Enums;

namespace ClientService.Domain.Entities;

public class Notification : BaseAuditableEntity
{
    [Key]
    public long Id { get; set; }
    
    public NotificationType Type { get; set; }

    public string Content { get; set; } = default!;
    
    public string? Data { get; set; }
    
    public long? AccountId { get; set; }
    
    public Account? Account { get; set; }
    
    public DateTimeOffset? ReadAt { get; set; }

    public string? ReferenceId { get; set; }

    public Notification()
    {
    }

    public Notification(NotificationType type)
    {
        this.Type = type;
        this.Content = type.GetDescription();
    }
}