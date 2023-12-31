﻿using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using ClientService.Application.Common.Persistence;

namespace ClientService.Application.Posts.Handler
{
    public class GetDetailPostApplicationHandler : IRequestHandler<GetDetailPostApplicationRequest, PostApplicationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDetailPostApplicationHandler> _logger;
        private readonly IMapper _mapper;

        public GetDetailPostApplicationHandler(IUnitOfWork unitOfWork, ILogger<GetDetailPostApplicationHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostApplicationResponse> Handle(GetDetailPostApplicationRequest request, CancellationToken cancellationToken)
        {
            var postApplications = await _unitOfWork.PostApplicationRepository.GetAsync(x => x.PostId == request.PostId && x.Id == request.ApplicationId);
            List<PostApplicationResponse> postApplicationResponses = new List<PostApplicationResponse>();
            PostApplication? postApplication = postApplications.FirstOrDefault();
            if (postApplication == null)
            {
                throw new ApiException(ResponseCode.PostApplicationNotFound);
            }

            return _mapper.Map<PostApplicationResponse>(postApplication);
        }
    }
}
