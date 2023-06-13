using AutoMapper;
using ClientService.Application.Notifications.Models;
using ClientService.Application.Notifications.Queries;
using ClientService.Domain.Entities;

namespace ClientService.Application.Notifications.Handlers;

public class GetAllNotificationHandler : IRequestHandler<GetAllNotificationRequest, PaginationResponse<Notification, NotificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public GetAllNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<PaginationResponse<Notification, NotificationResponse>> Handle(GetAllNotificationRequest request, CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        request.AccountId = currentAccount.Id;

        var query = await _unitOfWork.NotificationRepository.GetAsync(
            predicate: request.GetExpressions(),
            orderBy: request.GetOrder()
        );

        return new PaginationResponse<Notification, NotificationResponse>(
            query,
            request.PageNumber,
            request.PageSize,
            noti => _mapper.Map<NotificationResponse>(noti));

    }
}