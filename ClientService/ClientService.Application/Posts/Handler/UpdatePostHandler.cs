using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;
using ClientService.Application.Common.Extensions;
using ClientService.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Posts.Handler
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostRequest, PostResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePostHandler> _logger;
        private readonly IMapper _mapper;

        public UpdatePostHandler(IUnitOfWork unitOfWork, ILogger<CreatePostHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PostResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var postQuery = await _unitOfWork.PostRepository.GetAsync(post => post.Id == request.Id);
            var post = await postQuery.FirstOrDefaultAsync(cancellationToken);

            if (post == null)
            {
                throw new ApiException(ResponseCode.PostNotFound);
            }

            post.Title = request.Title ?? post.Title;
            post.JobPosition = request.JobPosition ?? post.JobPosition;
            post.JobLevel = request.JobLevel ?? post.JobLevel;
            post.ServiceType = request.ServiceType ?? post.ServiceType;
            post.FinishTime = request.FinishTime ?? post.FinishTime;
            post.Content = request.Content ?? post.Content;
            post.SupportCount = request.SupportCount ?? post.SupportCount;
            post.MediaUrl = request.MediaUrl ?? post.MediaUrl;
            post.CVStyle = request.CVStyle ?? post.CVStyle;
            post.CVType = request.CVType ?? post.CVType;

            if (request.MajorCode != null)
            {
                var majorCode = request.MajorCode;
                var majorQuery = await _unitOfWork.MajorRepository.GetAsync(
                    m => m.Code.Equals(majorCode) || m.Code.Equals(majorCode.ToCode()));

                var major = majorQuery.FirstOrDefault() ?? new Major
                {
                    Name = majorCode,
                    Code = majorCode.ToCode()
                };

                post.Major = major;
            }
            
            await _unitOfWork.PostRepository.UpdateAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PostResponse>(post);
        }
    }
}
