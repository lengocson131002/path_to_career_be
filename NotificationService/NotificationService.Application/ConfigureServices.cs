using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Common.Behaviours;
using NotificationService.Application.Common.Mappings;

namespace NotificationService.Application;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Auto mapper
        services.AddAutoMapper(typeof(MappingProfiles));
        
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