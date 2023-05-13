using System.Reflection;
using FluentValidation;
using IdentityService.Application.Common.Behaviours;
using IdentityService.Application.Common.Mappings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application;

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