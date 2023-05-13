using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        return services;
    }
}