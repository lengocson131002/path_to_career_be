using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Posts.Handler
{
    public class GetAllPostInPageHandler : IRequestHandler<GetAllPostInPageRequest, PaginationResponse<Post, PostResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllPostInPageHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllPostInPageHandler(IUnitOfWork unitOfWork, ILogger<GetAllPostInPageHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PaginationResponse<Post, PostResponse>> Handle(GetAllPostInPageRequest request, CancellationToken cancellationToken)
        {
            var posts = await _unitOfWork.PostRepository.GetAsync(
                predicate: request.GetExpressions(),
                orderBy: request.GetOrder());

            return new PaginationResponse<Post, PostResponse>(posts, request.PageNumber, request.PageSize, entity => _mapper.Map<PostResponse>(entity));
        }
    }
}
