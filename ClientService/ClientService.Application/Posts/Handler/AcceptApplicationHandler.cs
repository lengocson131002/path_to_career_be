using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Posts.Handler
{
    public class AcceptApplicationHandler : IRequestHandler<AcceptApplicationRequest, StatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AcceptApplicationHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AcceptApplicationHandler(IUnitOfWork unitOfWork, ILogger<AcceptApplicationHandler> logger, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<StatusResponse> Handle(AcceptApplicationRequest request, CancellationToken cancellationToken)
        {
            Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
            if (post == null || post.AcceptedAccountId != null)
            {
                return new StatusResponse(false);
            }
            var postApplicationQuery = await _unitOfWork.PostApplicationRepository.GetAsync(x => x.Id == request.ApplicationId);
            var postApplication = await postApplicationQuery.FirstOrDefaultAsync(cancellationToken);
            if (postApplication == null || postApplication.ApplicationStatus == ApplicationStatus.Cancel)
            {
                throw new ApiException(ResponseCode.PostApplicationNotFound);
            }
            post.AcceptedAccountId = postApplication.ApplierId;
            post.Status = Domain.Enums.PostStatus.Done;

            await _unitOfWork.PostRepository.UpdateAsync(post);
            await _unitOfWork.SaveChangesAsync();
            
            return new StatusResponse(true);
        }
    }
}
