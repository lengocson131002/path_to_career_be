using ClientService.Application.Services.Models;

namespace ClientService.Application.Services.Commands;

public class RemoveServiceRequest : IRequest<ServiceResponse>
{
    public long Id { get; set; }
}