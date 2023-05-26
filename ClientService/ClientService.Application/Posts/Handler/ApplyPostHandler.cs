using AutoMapper;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using ClientService.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Handler
{
    public class ApplyPostHandler : IRequestHandler<ApplyPostRequest, ApplyPostResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApplyPostHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ApplyPostHandler(IUnitOfWork unitOfWork, ILogger<ApplyPostHandler> logger, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _currentUserService = currentUserService;
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
            var postApplication = _mapper.Map<PostApplication>(request);
            postApplication.ApplicationStatus = ApplicationStatus.Open;
            await _unitOfWork.PostApplicationRepository.AddAsync(postApplication);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ApplyPostResponse>(postApplication);
        }
    }
}
