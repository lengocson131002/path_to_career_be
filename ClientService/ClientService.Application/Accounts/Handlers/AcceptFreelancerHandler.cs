using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Domain.Entities;

namespace ClientService.Application.Accounts.Handlers;

public class AcceptFreelancerHandler : IRequestHandler<AcceptFreelancerRequest, StatusResponse>
{
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptFreelancerHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<StatusResponse> Handle(AcceptFreelancerRequest request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.Id);
        if (account == null) throw new ApiException(ResponseCode.AccountErrorNotFound);

        if (!account.Role.Equals(Role.Freelancer) || account.IsAccepted) return new StatusResponse(false);

        account.IsAccepted = true;
        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync();

        var notification = new Notification(NotificationType.FreelancerAccepted)
        {
            AccountId = account.Id,
            Data = JsonSerializer.Serialize(account, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }),
            ReferenceId = account.Id.ToString()
        };
        await _notificationService.PushNotification(notification);

        return new StatusResponse(true);
    }
}