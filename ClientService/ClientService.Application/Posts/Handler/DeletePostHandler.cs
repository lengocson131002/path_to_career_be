using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Application.Common.Persistence;

namespace ClientService.Application.Posts.Handler
{
    public class DeletePostHandler : IRequestHandler<DeletePostRequest, StatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePostHandler> _logger;
        private readonly IMapper _mapper;

        public DeletePostHandler(IUnitOfWork unitOfWork, ILogger<DeletePostHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<StatusResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);
            if (post == null)
            {
                return new StatusResponse(false);
            }

            post.Status = PostStatus.Done;
            
            await _unitOfWork.PostRepository.UpdateAsync(post);
            await _unitOfWork.SaveChangesAsync();
            
            return new StatusResponse(true);
        }
    }
}
