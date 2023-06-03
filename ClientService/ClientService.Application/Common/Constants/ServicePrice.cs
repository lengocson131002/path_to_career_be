namespace ClientService.Application.Common.Constants;

public static class ServicePrice
{
    public static IDictionary<ServiceType, decimal> ServicePrices = new Dictionary<ServiceType, decimal>()
    {
        { ServiceType.ReviewCV , 50000 },
        { ServiceType.CreateCV, 100000 },
        { ServiceType.MockInterview , 200000}
    };
}