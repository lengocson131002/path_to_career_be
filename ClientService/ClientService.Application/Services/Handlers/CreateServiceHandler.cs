using AutoMapper;
using ClientService.Application.Services.Commands;
using ClientService.Application.Services.Models;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Services.Handlers;

public class CreateServiceHandler : IRequestHandler<CreateServiceRequest, ServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;
    
    public CreateServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> Handle(CreateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = _mapper.Map<Service>(request);
        
        // Check exist name
        var serviceQuery = await _unitOfWork.ServiceRepository.GetAsync(s => s.Name.ToLower().Equals(service.Name.ToLower()));
        if (await serviceQuery.AnyAsync(cancellationToken))
        {
            throw new ApiException(ResponseCode.ServiceErrorExistedName);
        }
        
        await _unitOfWork.ServiceRepository.AddAsync(service);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ServiceResponse>(service);
    }
}