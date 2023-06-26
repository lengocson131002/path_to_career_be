namespace ClientService.Application.Dashboard.Models;

public class AccountStatisticResponse : Dictionary<Role, AccountStatistic>
{
    
}

public class AccountStatistic
{
    public int Active { get; set; }
    
    public int Inactive { get; set; }
}