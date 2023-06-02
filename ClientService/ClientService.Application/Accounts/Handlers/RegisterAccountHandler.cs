using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Extensions;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Accounts.Handlers;

public class RegisterAccountHandler : IRequestHandler<RegisterAccountRequest, AccountResponse>
{
    private readonly ILogger<RegisterAccountHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterAccountHandler(IUnitOfWork unitOfWork, ILogger<RegisterAccountHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AccountResponse> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        var accountQuery = await _unitOfWork.AccountRepository.GetAsync(
            acc => acc.Email.ToLower().Equals(request.Email.ToLower()));

        var existedAccount = accountQuery.FirstOrDefault();
        if (existedAccount != null) throw new ApiException(ResponseCode.AccountErrorEmailExisted);

        var account = _mapper.Map<Account>(request);
        account.Majors = new List<Major>();

        // majors
        foreach (var majorCode in request.MajorCodes)
            if (!string.IsNullOrWhiteSpace(majorCode))
            {
                var majorQuery = await _unitOfWork.MajorRepository.GetAsync(
                    m => m.Code.Equals(majorCode) || m.Code.Equals(majorCode.ToCode()));

                var major = majorQuery.FirstOrDefault() ?? new Major
                {
                    Name = majorCode,
                    Code = majorCode.ToCode()
                };

                account.Majors.Add(major);
            }

        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Create new Account: {0}", account.Email);

        return _mapper.Map<AccountResponse>(account);
    }
}