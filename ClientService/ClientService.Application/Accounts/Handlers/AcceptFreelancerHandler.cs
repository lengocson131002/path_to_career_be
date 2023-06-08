using AutoMapper;
using ClientService.Application.Accounts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Accounts.Handlers
{
    public class AcceptFreelancerHandler : IRequestHandler<AcceptFreelancerRequest, StatusResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AcceptFreelancerHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentAccountService currentAccountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<StatusResponse> Handle(AcceptFreelancerRequest request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.Id);
            if (account == null)
            {
                throw new ApiException(ResponseCode.AccountErrorNotFound);
            }
            if (account.Role.Equals(Role.Freelancer) && account.IsAccepted == false)
            {
                account.IsAccepted = true;
                await _unitOfWork.AccountRepository.UpdateAsync(account);
                _unitOfWork.SaveChanges();
                return new StatusResponse(true);
            } 
            else
            {
                return new StatusResponse(false);
            }
        }
    }
}
