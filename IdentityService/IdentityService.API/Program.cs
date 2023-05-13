using IdentityService.API;
using IdentityService.API.Filters;
using IdentityService.Application;
using IdentityService.Application.Common.Exceptions;
using IdentityService.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    
    // Swagger doc
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "P2C Api",
        Version = "v1"
    });

    //Security Definition
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    // Filter security requirement
    opt.OperationFilter<AuthorizationOperationFilter>();
    
});

builder.Services.ConfigureApiServices(builder.Configuration);

builder.Services.ConfigureApplicationServices(builder.Configuration);

builder.Services.ConfigureInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseGrpcEndpoints();

app.Run();