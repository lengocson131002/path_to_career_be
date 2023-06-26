using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ClientService.Application.Common.Constants;
using ClientService.Application.Common.Extensions;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Posts.Handler;

public class CreatePostHandler : IRequestHandler<CreatePostRequest, PostResponse>
{
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ILogger<CreatePostHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public CreatePostHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreatePostHandler> logger,
        IMapper mapper, ICurrentAccountService currentAccountService, INotificationService notificationService, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _notificationService = notificationService;
        _configuration = configuration;
    }

    public async Task<PostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(request);

        var majorCode = request.MajorCode;
        var majorQuery = await _unitOfWork.MajorRepository.GetAsync(
            m => m.Code.Equals(majorCode) || m.Code.Equals(majorCode.ToCode()));

        var major = majorQuery.FirstOrDefault() ?? new Major
        {
            Name = majorCode,
            Code = majorCode.ToCode()
        };

        post.Major = major;

        var currentAccount = await _currentAccountService.GetCurrentAccount();
        post.Account = currentAccount;
        
        // check if it is free
        var freeCountConfig = _configuration["Settings:FreeCount"];
        var freeCount = freeCountConfig != null ? int.Parse(freeCountConfig) : Settings.FreePostCount;
        
        var postQuery = await _unitOfWork.PostRepository.GetAsync(p => p.AccountId == currentAccount.Id);
        var postCount = await postQuery.CountAsync(cancellationToken);
        await _unitOfWork.PostRepository.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();
        
        if (postCount < freeCount)
        {
            _logger.LogInformation("Free post");
            
            post.Status = PostStatus.Paid;
            
            // notify freelancer
            var freelancerQuery = await _unitOfWork.AccountRepository.GetAsync(acc => Role.Freelancer.Equals(acc.Role));
            var freelancers = await freelancerQuery.ToListAsync(cancellationToken);
            foreach (var freelancer in freelancers)
            {
                var notification = new Notification(NotificationType.PostCreated)
                {
                    AccountId = freelancer.Id,
                    Data = JsonSerializer.Serialize(post, new JsonSerializerOptions()
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    }),
                    ReferenceId = post.Id.ToString(),
                };
                await _notificationService.PushNotification(notification);
            }
        }
        
        return _mapper.Map<PostResponse>(post);
    }
}