using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Services.Models;
using ClientService.Application.Services.Queries;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Services.Handlers;

public class GetServiceDetailHandler : IRequestHandler<GetServiceDetailRequest, ServiceDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetServiceDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceDetailResponse> Handle(GetServiceDetailRequest request, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.ServiceRepository.GetByIdAsync(request.Id);
        if (service == null)
        {
            throw new ApiException(ResponseCode.ServiceErrorNotFound);
        }

        var response = _mapper.Map<ServiceDetailResponse>(service);
        
        // Count registered account
        var registrationQuery = await _unitOfWork.AccountServiceRepository.GetAsync(reg => reg.ServiceId == service.Id);
        response.AccountCount = await registrationQuery.CountAsync(cancellationToken);

        return response;
    }
}