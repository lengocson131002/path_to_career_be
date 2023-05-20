using AutoMapper;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
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
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, PostResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePostHandler> _logger;
        private readonly IMapper _mapper;

        public CreatePostHandler(IUnitOfWork unitOfWork, ILogger<CreatePostHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PostResponse>(post);
        }
    }
}
