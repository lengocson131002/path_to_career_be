using AutoMapper;
using ClientService.Application.Services.Models;
using ClientService.Application.Services.Queries;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Services.Handlers;

public class GetAllServicesHandler : IRequestHandler<GetAllServicesRequest, ListResponse<ServiceResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllServicesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ListResponse<ServiceResponse>> Handle(GetAllServicesRequest request, CancellationToken cancellationToken)
    {
        //Get only active services
        var serviceQuery = await _unitOfWork.ServiceRepository.GetAsync(service => service.DeletedAt == null);
        var services = await serviceQuery
            .OrderBy(service => service.Order)
            .ToListAsync(cancellationToken);
        
        var response = _mapper.Map<List<ServiceResponse>>(services);

        return new ListResponse<ServiceResponse>(response);
    }
}