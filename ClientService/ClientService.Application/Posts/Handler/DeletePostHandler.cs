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
    public class DeletePostHandler : IRequestHandler<DeletePostRequest, DeletePostResponse>
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

        public async Task<DeletePostResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            DeletePostResponse response = new DeletePostResponse();
            var post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);
            if (post == null)
            {
                return response;
            }
            await _unitOfWork.PostRepository.DeleteAsync(post);
            await _unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }
    }
}
