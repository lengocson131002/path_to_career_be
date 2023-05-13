using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        return services;
    }
}