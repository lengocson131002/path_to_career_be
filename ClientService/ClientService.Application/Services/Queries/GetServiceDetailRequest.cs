using ClientService.Application.Services.Models;

namespace ClientService.Application.Services.Queries;

public class GetServiceDetailRequest : IRequest<ServiceDetailResponse>
{
    public long Id { get; set; }
}