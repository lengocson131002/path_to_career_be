using ClientService.Application.Common.Interfaces;
using ClientService.Application.Greeting.Commands;
using ClientService.Application.Greeting.Models;
using MediatR;

namespace ClientService.Application.Greeting.Handlers;

public class GreetingHandler : IRequestHandler<GreetingRequest, GreetingResponse>
{
    private readonly IGreetingService _greetingService;

    public GreetingHandler(IGreetingService greetingService)
    {
        _greetingService = greetingService;
    }

    public Task<GreetingResponse> Handle(GreetingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GreetingResponse()
        {
            Message = _greetingService.Greeting(request.Name)
        });
    }
}