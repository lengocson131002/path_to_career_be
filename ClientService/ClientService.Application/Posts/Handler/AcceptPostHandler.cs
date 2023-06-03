namespace ClientService.Application.Posts.Handler;

public class AcceptPostHandler : IRequestHandler<AcceptPostRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentAccountService _currentAccountService;

    public AcceptPostHandler(IUnitOfWork unitOfWork, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _currentAccountService = currentAccountService;
    }

    public async Task<StatusResponse> Handle(AcceptPostRequest request, CancellationToken cancellationToken)
    {
        // Validate post
        var post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }

        if (!PostStatus.Paid.Equals(post.Status))
        {
            throw new ApiException(ResponseCode.InvalidPostStatus);
        }

        // if (post.TransactionId == null)
        // {
        //     throw new ApiException(ResponseCode.InvalidPostStatus);
        // }

        var currentAccount = await _currentAccountService.GetCurrentAccount();
        post.Freelancer = currentAccount;
        post.Status = PostStatus.Accepted;
        
        // Todo notification

        await _unitOfWork.PostRepository.UpdateAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return new StatusResponse(true);
    }
}