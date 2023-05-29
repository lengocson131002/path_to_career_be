using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Extensions;
using ClientService.Application.Common.Interfaces;
using ClientService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Accounts.Handlers;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountRequest, AccountResponse>
{
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ILogger<UpdateAccountHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountHandler(
        IUnitOfWork unitOfWork,
        ILogger<UpdateAccountHandler> logger,
        IMapper mapper,
        ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<AccountResponse> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _currentAccountService.GetCurrentAccount( new List<Expression<Func<Account, object>>>()
        {
            acc => acc.Majors
        });

        account.Avatar = request.Avatar;
        account.PhoneNumber = request.PhoneNumber ?? account.PhoneNumber;
        account.FullName = request.FullName ?? account.FullName;
        account.Description = request.Description ?? account.Description;

        if (request.MajorCodes != null)
        {
            account.Majors.Clear();
            foreach (var majorCode in request.MajorCodes)
                if (!string.IsNullOrWhiteSpace(majorCode))
                {
                    var majorQuery = await _unitOfWork.MajorRepository.GetAsync(m =>
                        m.Code.Equals(majorCode) || m.Code.Equals(majorCode.ToCode()));

                    var major = majorQuery.FirstOrDefault() ?? new Major
                    {
                        Name = majorCode,
                        Code = majorCode.ToCode()
                    };

                   account.Majors.Add(major);
                }
        }

        await _unitOfWork.AccountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AccountResponse>(account);
    }
}