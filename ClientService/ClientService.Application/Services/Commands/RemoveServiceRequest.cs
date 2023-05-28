using ClientService.Application.Services.Models;
using MediatR;

namespace ClientService.Application.Services.Commands;

public class RemoveServiceRequest : IRequest<ServiceResponse>
{
    public long Id { get; set; }
}