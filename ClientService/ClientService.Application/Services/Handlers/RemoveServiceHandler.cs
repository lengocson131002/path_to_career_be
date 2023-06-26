using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Services.Commands;
using ClientService.Application.Services.Models;

namespace ClientService.Application.Services.Handlers;

public class RemoveServiceHandler : IRequestHandler<RemoveServiceRequest, ServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public RemoveServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<ServiceResponse> Handle(RemoveServiceRequest request, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.ServiceRepository.GetByIdAsync(request.Id);
        if (service == null || !service.IsActive)
        {
            throw new ApiException(ResponseCode.ServiceErrorNotFound);
        }

        var currentAccount =await _currentAccountService.GetCurrentAccount();
        
        service.DeletedAt = DateTimeOffset.UtcNow;
        service.DeletedBy = currentAccount.Email;

        await _unitOfWork.ServiceRepository.UpdateAsync(service);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ServiceResponse>(service);
    }
}