using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Posts.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Posts.Handler;

public class FreelancerCancelApplicationHandler : IRequestHandler<FreelancerCancelApplicationRequest, PostApplicationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public FreelancerCancelApplicationHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<PostApplicationResponse> Handle(FreelancerCancelApplicationRequest request, CancellationToken cancellationToken)
    {
        var postQuery = await _unitOfWork.PostRepository.GetAsync(post => post.Id == request.PostId);
        var post = await postQuery.FirstOrDefaultAsync(cancellationToken);
        if (post == null)
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }

        var account = _currentAccountService.GetCurrentAccount();
        
        var applicationQuery = await _unitOfWork.PostApplicationRepository.GetAsync(application => application.PostId == post.Id && application.ApplierId == account.Id);
        var application = await applicationQuery.FirstOrDefaultAsync(cancellationToken);
        if (application == null)
        {
            throw new ApiException(ResponseCode.PostApplicationNotFound);
        }

        application.ApplicationStatus = ApplicationStatus.Cancel;
        await _unitOfWork.PostApplicationRepository.UpdateAsync(application);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<PostApplicationResponse>(application);
    }
}