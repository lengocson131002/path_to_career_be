using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Services.Commands;
using ClientService.Application.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Services.Handlers;

public class UpdateServiceHandler : IRequestHandler<UpdateServiceRequest, ServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;


    public UpdateServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.ServiceRepository.GetByIdAsync(request.Id);
        if (service == null)
        {
            throw new ApiException(ResponseCode.ServiceErrorNotFound);
        }
        
        // check exist name
        if (request.Name != null && string.Compare(request.Name, service.Name, StringComparison.OrdinalIgnoreCase) != 0)
        {
            var serviceQuery = await _unitOfWork.ServiceRepository.GetAsync(s => s.Name.ToLower().Equals(request.Name.ToLower()));
            if (await serviceQuery.AnyAsync(cancellationToken))
            {
                throw new ApiException(ResponseCode.ServiceErrorExistedName);
            }

            service.Name = request.Name;
        }
        
        service.Name = request.Name ?? service.Name;
        service.Price = request.Price ?? service.Price;
        service.Discount = request.Discount ?? service.Discount;
        service.ApplyCount = request.ApplyCount ?? service.ApplyCount;
        service.Duration = request.Duration ?? service.Duration;
        service.OnTop = request.OnTop ?? service.OnTop;
        service.Order = request.Order ?? service.Order;
        service.Recommended = request.Recommended ?? service.Recommended;
        service.Description = request.Description ?? service.Description;

        await _unitOfWork.ServiceRepository.UpdateAsync(service);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ServiceResponse>(service);
    }
}