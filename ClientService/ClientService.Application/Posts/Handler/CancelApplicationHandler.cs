using AutoMapper;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using ClientService.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Posts.Handler
{
    public class CancelApplicationHandler : IRequestHandler<CancelApplicationRequest, PostApplicationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CancelApplicationHandler> _logger;
        private readonly IMapper _mapper;

        public CancelApplicationHandler(IUnitOfWork unitOfWork, ILogger<CancelApplicationHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PostApplicationResponse> Handle(CancelApplicationRequest request, CancellationToken cancellationToken)
        {
            var applicationQuery = await _unitOfWork.PostApplicationRepository.GetAsync(application => application.PostId == request.PostId && application.Id == request.ApplicationId);
            var application = await applicationQuery.FirstOrDefaultAsync(cancellationToken);

            if (application == null)
            {
                throw new ApiException(ResponseCode.PostApplicationNotFound);
            }

            application.ApplicationStatus = ApplicationStatus.Cancel;

            await _unitOfWork.PostApplicationRepository.UpdateAsync(application);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<PostApplicationResponse>(application);
        }
    }
}
