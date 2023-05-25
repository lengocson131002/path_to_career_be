using AutoMapper;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
using ClientService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Handler
{
    public class GetAllPostApplicationInPageHandler : IRequestHandler<GetAllApplicationRequest, PostApplicationPageResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllPostApplicationInPageHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllPostApplicationInPageHandler(IUnitOfWork unitOfWork, ILogger<GetAllPostApplicationInPageHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostApplicationPageResponse> Handle(GetAllApplicationRequest request, CancellationToken cancellationToken)
        {
            var postApplications = await _unitOfWork.PostApplicationRepository.GetAsync(x => x.PostId == request.PostId);
            List<PostApplicationResponse> postApplicationResponses = new List<PostApplicationResponse>();
            foreach (var postApplication in postApplications)
            {
                postApplicationResponses.Add(_mapper.Map<PostApplicationResponse>(postApplication));
            }
            
            PostApplicationPageResponse response = new PostApplicationPageResponse(postApplicationResponses, postApplicationResponses.Count(), request.PageNumber, request.PageSize);
           
            return response;
        }
    }
}
