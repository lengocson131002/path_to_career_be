using AutoMapper;
using ClientService.Application.Common.Extensions;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Posts.Handler;

public class CreatePostHandler : IRequestHandler<CreatePostRequest, PostResponse>
{
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ILogger<CreatePostHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreatePostHandler> logger,
        IMapper mapper, ICurrentAccountService currentAccountService, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _notificationService = notificationService;
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

        await _unitOfWork.PostRepository.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PostResponse>(post);
    }
}