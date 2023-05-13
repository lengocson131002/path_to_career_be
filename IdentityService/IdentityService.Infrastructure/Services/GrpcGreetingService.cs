using Grpc.Core;
using GrpcService;

namespace IdentityService.Infrastructure.Services;

public class GrpcGreetingService : Greeter.GreeterBase
{
    public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloResponse()
        {
            Message = "Hello " + request.Name
        });
    }
}