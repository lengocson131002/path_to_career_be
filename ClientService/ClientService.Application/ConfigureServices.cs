using System.Reflection;
using ClientService.Application.Common.Behaviours;
using ClientService.Application.Common.Mappings;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientService.Application;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Auto mapper
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddAutoMapper(typeof(PostProfiles));
        
        // FluentAPI validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationErrorBehaviour<,>));
        });
        
        return services;
    }
}