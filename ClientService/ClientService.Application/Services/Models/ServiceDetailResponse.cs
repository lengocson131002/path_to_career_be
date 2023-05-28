namespace ClientService.Application.Services.Models;

public class ServiceDetailResponse : ServiceResponse
{
    public int AccountCount { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
    
    public string? UpdatedBy { get; set; }
    
    public DateTimeOffset? DeletedAt { get; set; }
    
    public string? DeletedBy { get; set; }
}