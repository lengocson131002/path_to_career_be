using System.ComponentModel.DataAnnotations;
using ClientService.Domain.Enums;

namespace ClientService.Domain.Entities;

public class Message : BaseAuditableEntity
{
    [Key]
    public long Id { get; set; }
    
    public MessageType Type { get; set; }

    public string Content { get; set; } = default!;
    
    public long PostId { get; set; }

    public Post Post { get; set; } = default!;
    
    public long AccountId { get; set; }

    public Account Account { get; set; } = default!;
}