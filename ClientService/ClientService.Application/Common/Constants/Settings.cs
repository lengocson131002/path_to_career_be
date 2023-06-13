namespace ClientService.Application.Common.Constants;

public static class Settings
{
    public const int FreePostCount = 3;
    
    public static IDictionary<ServiceType, decimal> ServicePrices = new Dictionary<ServiceType, decimal>()
    {
        { ServiceType.ReviewCV , 50000 },
        { ServiceType.CreateCV, 100000 },
        { ServiceType.MockInterview , 200000}
    };
}