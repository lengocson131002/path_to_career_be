using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Persistence;
using ClientService.Application.Dashboard.Queries;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Dashboard.Handlers;

public class GetTopFreelancerHandler : IRequestHandler<GetTopFreelancerRequest, ListResponse<AccountResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTopFreelancerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ListResponse<AccountResponse>> Handle(GetTopFreelancerRequest request, CancellationToken cancellationToken)
    {
        var freelancerQuery = await _unitOfWork.PostRepository.GetAsync(
            predicate: post => post.Freelancer != null,
            includes: new ListResponse<Expression<Func<Post, object>>>()
            {
                post => post.Freelancer
            });

        var postCountStatistic = await freelancerQuery
            .GroupBy(x => x.Freelancer)
            .Select(x => new
            {
                Account = x.Key,
                Count = x.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(request.Top ?? 10)
            .ToListAsync(cancellationToken);

        var response = new ListResponse<AccountResponse>();
        foreach (var item in postCountStatistic)
        {
            var acc = _mapper.Map<AccountResponse>(item.Account);
            acc.PostCount = item.Count;
            response.Add(acc);
        }

        return response;
    }
}