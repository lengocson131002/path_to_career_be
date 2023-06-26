using ClientService.Application.Common.Persistence;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Posts.Handler;

public class CompletePostHandler : IRequestHandler<CompletePostRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompletePostRequest> _logger;
    private readonly ICurrentAccountService _currentAccountService;

    public CompletePostHandler(IUnitOfWork unitOfWork, ICurrentAccountService currentAccountService, ILogger<CompletePostRequest> logger)
    {
        _unitOfWork = unitOfWork;
        _currentAccountService = currentAccountService;
        _logger = logger;
    }

    public async Task<StatusResponse> Handle(CompletePostRequest request, CancellationToken cancellationToken)
    {
        // Validate post
        var post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }
        
        if (!PostStatus.Accepted.Equals(post.Status))
        {
            throw new ApiException(ResponseCode.InvalidPostStatus);
        }
        
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        if (post.Freelancer != null && currentAccount.Id != post.Freelancer.Id)
        {
            _logger.LogError("Failed action. Current account is not post's accepted freelancer");
            throw new ApiException(ResponseCode.ErrorCommon);
        }
        
        post.Status = PostStatus.Done;
        
        await _unitOfWork.PostRepository.UpdateAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return new StatusResponse(true);
    }
}