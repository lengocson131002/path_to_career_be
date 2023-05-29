using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Accounts.Queries;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Common.Models.Response;
using ClientService.Domain.Entities;
using MediatR;

namespace ClientService.Application.Accounts.Handlers;

public class GetAllAccountsHandler : IRequestHandler<GetAllAccountRequest, PaginationResponse<Account, AccountResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;

    public GetAllAccountsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<Account, AccountResponse>> Handle(GetAllAccountRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.AccountRepository.GetAsync(
            predicate: request.GetExpressions(),
            orderBy: request.GetOrder(),
            includes: new List<Expression<Func<Account, object>>>()
            {
                acc => acc.Majors,
                acc => acc.Reviews
            }
        );
        
        // Default sort by total rating score
        var response = query.Select(item => new AccountResponse()
            {
                Id = item.Id,
                Avatar = item.Avatar,
                Email = item.Email,
                PhoneNumber = item.FullName,
                FullName = item.FullName,
                Role = item.Role,
                Description = item.Description,
                Score = item.Reviews.Any() ? item.Reviews.Sum(r => r.Score) / item.Reviews.Count : 0
            })
            .OrderByDescending(item => item.Score)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
        
        return new PaginationResponse<Account, AccountResponse>(response, query.Count(), request.PageNumber, request.PageSize);
    }
}