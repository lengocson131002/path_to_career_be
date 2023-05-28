using ClientService.Application.Common.Models.Response;
using ClientService.Application.Services.Models;
using MediatR;

namespace ClientService.Application.Services.Queries;

public class GetAllServicesRequest : IRequest<ListResponse<ServiceResponse>>
{
}