namespace ClientService.Application.Dashboard.Models;

public class PostStatisticResponse 
{
    public Dictionary<PostStatus, int> ByStatus { get; set; } = new();
    
    public Dictionary<ServiceType, int> ByService { get; set; } = new();
}
