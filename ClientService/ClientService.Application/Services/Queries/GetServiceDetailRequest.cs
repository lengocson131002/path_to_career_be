using ClientService.Application.Services.Models;
using MediatR;

namespace ClientService.Application.Services.Queries;

public class GetServiceDetailRequest : IRequest<ServiceDetailResponse>
{
    public long Id { get; set; }
}