using AutoMapper;
using ClientService.Application.Notifications.Commands;
using ClientService.Application.Notifications.Models;

namespace ClientService.Application.Notifications.Handlers;

public class RemoveNotificationHandler : IRequestHandler<RemoveNotificationRequest, NotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public RemoveNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<NotificationResponse> Handle(RemoveNotificationRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.Id);
        if (notification == null || (currentAccount.Id != notification.AccountId))
        {
            throw new ApiException(ResponseCode.NotificationNotFound);
        }

        await _unitOfWork.NotificationRepository.DeleteAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<NotificationResponse>(notification);
    }
}