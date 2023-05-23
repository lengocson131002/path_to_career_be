using AutoMapper;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Handler
{
    public class GetDetailPostHandler : IRequestHandler<GetDetailPostRequest, PostResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDetailPostHandler> _logger;
        private readonly IMapper _mapper;

        public GetDetailPostHandler(IUnitOfWork unitOfWork, ILogger<GetDetailPostHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(GetDetailPostRequest request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);
            return _mapper.Map<PostResponse>(post);
        }
    }
}
