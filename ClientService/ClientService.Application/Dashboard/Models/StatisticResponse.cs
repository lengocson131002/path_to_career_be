namespace ClientService.Application.Dashboard.Models;

public class StatisticResponse
{
    public int UserCount { get; set; }
    
    public int FreelancerCount { get; set; }
    
    public int PostCount { get; set; }
    
    public decimal Revenue { get; set; }
}