using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClientService.Application.Common.Persistence;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Posts.Handler
{
    public class GetDetailPostHandler : IRequestHandler<GetDetailPostRequest, PostDetailResponse>
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

        public async Task<PostDetailResponse> Handle(GetDetailPostRequest request, CancellationToken cancellationToken)
        {
            var postQuery = await _unitOfWork.PostRepository.GetAsync(
                predicate: post => post.Id == request.Id,
                includes: new ListResponse<Expression<Func<Post, object>>>()
                {
                    post => post.Major,
                    post => post.Account,
                    post => post.AcceptedAccount!,
                    post => post.Freelancer!,
                    post => post.Transaction!,
                    post => post.Review!
                });
            var post = await postQuery.FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<PostDetailResponse>(post);
        }
    }
}
