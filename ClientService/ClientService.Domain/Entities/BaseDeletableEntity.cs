namespace ClientService.Domain.Entities;

public class BaseDeletableEntity
{
    public DateTimeOffset DeletedAt { get; set; }
    
    public string? DeletedBy { get; set; }
}