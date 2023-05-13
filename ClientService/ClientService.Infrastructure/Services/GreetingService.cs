using ClientService.Application.Common.Interfaces;
using Grpc.Net.Client;
using GrpClient;
using Microsoft.Extensions.Configuration;

namespace ClientService.Infrastructure.Services;

public class GreetingService : IGreetingService
{
    private readonly string _grpcUrl;
    
    public GreetingService(IConfiguration configuration)
    {
        _grpcUrl = configuration["Grpc:Url"] ?? throw new ArgumentException("Grpc:Url is required");
    }
    public string Greeting(string name)
    {
        using var channel = GrpcChannel.ForAddress(_grpcUrl);
        var client = new Greeter.GreeterClient(channel);

        return client.SayHello(new HelloRequest()
        {
            Name = name
        }).Message;
    }
}