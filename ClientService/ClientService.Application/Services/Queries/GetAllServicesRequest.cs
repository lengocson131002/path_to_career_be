using ClientService.Application.Services.Models;

namespace ClientService.Application.Services.Queries;

public class GetAllServicesRequest : IRequest<ListResponse<ServiceResponse>>
{
}