using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Extensions;
using ClientService.Application.Common.Persistence;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ClientService.Application.Accounts.Handlers;

public class RegisterAccountHandler : IRequestHandler<RegisterAccountRequest, AccountResponse>
{
    private readonly ILogger<RegisterAccountHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;
    
    public RegisterAccountHandler(
        IUnitOfWork unitOfWork, 
        ILogger<RegisterAccountHandler> logger, 
        IMapper mapper, 
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<AccountResponse> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(
            acc => acc.Email.ToLower().Equals(request.Email.ToLower()));

        var existedAccount = accountQuery.FirstOrDefault();
        if (existedAccount != null) throw new ApiException(ResponseCode.AccountErrorEmailExisted);

        var account = _mapper.Map<Account>(request);
        
        // set Accepted
        account.IsAccepted = !Role.Freelancer.Equals(account.Role);
        
        account.Majors = new List<Major>();

        // majors
        foreach (var majorCode in request.MajorCodes)
            if (!string.IsNullOrWhiteSpace(majorCode))
            {
                var majorQuery = await _unitOfWork.MajorRepository.GetAsync(
                    m => m.Code.Equals(majorCode) || m.Code.Equals(majorCode.ToCode()));

                var major = majorQuery.FirstOrDefault() ?? new Major
                {
                    Name = majorCode,
                    Code = majorCode.ToCode()
                };

                account.Majors.Add(major);
            }

        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<AccountResponse>(account);

        _logger.LogInformation("Create new Account: {0}", account.Email);

        // Push notification to admins
        if (Role.Freelancer.Equals(account.Role))
        {
            var adminQuery = await _unitOfWork.AccountRepository.GetAsync(acc => Role.Admin.Equals(acc.Role));
            var admins = await adminQuery.ToListAsync(cancellationToken);

            foreach (var admin in admins)
            {
                var notification = new Notification(NotificationType.FreelancerCreated)
                {
                    AccountId = admin.Id,
                    Data = JsonSerializer.Serialize(account, new JsonSerializerOptions()
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    }),
                    ReferenceId = account.Id.ToString(),
                };
                await _notificationService.PushNotification(notification);
            }
        }

        return response;
    }
}