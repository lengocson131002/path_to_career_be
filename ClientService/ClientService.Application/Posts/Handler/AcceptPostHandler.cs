using ClientService.Application.Common.Persistence;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Posts.Handler;

public class AcceptPostHandler : IRequestHandler<AcceptPostRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ILogger<AcceptPostHandler> _logger;

    public AcceptPostHandler(IUnitOfWork unitOfWork, ICurrentAccountService currentAccountService, ILogger<AcceptPostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _currentAccountService = currentAccountService;
        _logger = logger;
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

        var currentAccount = await _currentAccountService.GetCurrentAccount();
        post.Freelancer = currentAccount;
        post.Status = PostStatus.Accepted;
        
        // Todo notification

        try
        {
            await _unitOfWork.PostRepository.UpdateAsync(post);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries.Single();
            var clientValues = (Post)entry.Entity;
            var databaseEntry = entry.GetDatabaseValues();
            if (databaseEntry == null)
            {
                throw new ApiException(ResponseCode.PostNotFound);
            }
            var databaseValues = (Post)(databaseEntry.ToObject());
            if (PostStatus.Accepted.Equals(databaseValues.Status) && databaseValues.FreelancerId != null)
            {
                _logger.LogError("Post accepted by another freelancer");
                throw new ApiException(ResponseCode.InvalidPostStatus);
            }

            throw new ApiException(ResponseCode.ErrorCommon);
        }

        return new StatusResponse(true);
    }
}