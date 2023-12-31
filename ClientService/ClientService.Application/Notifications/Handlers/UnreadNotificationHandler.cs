using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Notifications.Commands;
using ClientService.Application.Notifications.Models;

namespace ClientService.Application.Notifications.Handlers;

public class UnreadNotificationHandler : IRequestHandler<UnreadNotificationRequest, NotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public UnreadNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<NotificationResponse> Handle(UnreadNotificationRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.Id);
        if (notification == null || (currentAccount.Id != notification.AccountId))
        {
            throw new ApiException(ResponseCode.NotificationNotFound);
        }

        notification.ReadAt = null;
        await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<NotificationResponse>(notification);
    }
}