using AutoMapper;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Posts.Handler
{
    public class ApplyPostHandler : IRequestHandler<ApplyPostRequest, ApplyPostResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApplyPostHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentAccountService _currentAccountService;

        public ApplyPostHandler(IUnitOfWork unitOfWork, ILogger<ApplyPostHandler> logger, IMapper mapper, ICurrentAccountService currentAccountService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _currentAccountService = currentAccountService;
        }

        public async Task<ApplyPostResponse> Handle(ApplyPostRequest request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
            if (post == null)
            {
                throw new ApiException(ResponseCode.PostNotFound);
            }
            
            if (post.Status.Equals(PostStatus.Done))
            {
                throw new ApiException(ResponseCode.PostIsDone);
            }

            var currentAccount = await _currentAccountService.GetCurrentAccount();

            var existApplicationQuery = await _unitOfWork.PostApplicationRepository.GetAsync(a => a.PostId == post.Id && a.ApplierId == currentAccount.Id);
            var existApplication = await existApplicationQuery.FirstOrDefaultAsync(cancellationToken);
            if (existApplication != null)
            {
                throw new ApiException(ResponseCode.ExistApplication);
            }
                
            var postApplication = _mapper.Map<PostApplication>(request);
            postApplication.Applier = currentAccount;
            
            postApplication.ApplicationStatus = ApplicationStatus.Open;
            
            await _unitOfWork.PostApplicationRepository.AddAsync(postApplication);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ApplyPostResponse>(postApplication);
        }
    }
}
