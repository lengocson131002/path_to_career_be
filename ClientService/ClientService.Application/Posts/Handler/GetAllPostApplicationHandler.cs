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
    public class GetAllPostApplicationHandler : IRequestHandler<GetAllApplicationRequest, PostApplicationListResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllPostApplicationHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllPostApplicationHandler(IUnitOfWork unitOfWork, ILogger<GetAllPostApplicationHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostApplicationListResponse> Handle(GetAllApplicationRequest request, CancellationToken cancellationToken)
        {
            var postApplications = await _unitOfWork.PostApplicationRepository.GetAsync(x => x.PostId == request.PostId);
            PostApplicationListResponse response = new PostApplicationListResponse();
            foreach (var postApplication in postApplications)
            {
                response.Add(_mapper.Map<PostApplicationResponse>(postApplication));
            }

            
           
            return response;
        }
    }
}
