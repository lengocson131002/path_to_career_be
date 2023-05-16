using System.Text;
using IdentityService.Application.Common.Interfaces;
using IdentityService.Infrastructure.Persistence;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ApplicationDbInitializer>();
        var scope = services.BuildServiceProvider().CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>();

        initializer.InitializeAsync().Wait();
        initializer.SeedAsync().Wait();
        
        // Authentication, Authorization
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Validate JWT Token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new ArgumentException("Jwt:Key is required")))
                };
            });
        
        
        // Unit Of Work 
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Jwt
        services.AddScoped<IJwtService, JwtService>();
        
        // Grpc
        services.AddGrpc();
        
        // Google Auth
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        
        return services;
    }

    public static IApplicationBuilder UseGrpcEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(e =>
        {
            e.MapGrpcService<GrpcGreetingService>();
        });
        
        return app;
    }
}