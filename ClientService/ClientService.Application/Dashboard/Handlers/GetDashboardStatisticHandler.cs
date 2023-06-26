using ClientService.Application.Common.Persistence;
using ClientService.Application.Dashboard.Models;
using ClientService.Application.Dashboard.Queries;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Dashboard.Handlers;

public class GetDashboardStatisticHandler : IRequestHandler<GetDashboardStatisticRequest, StatisticResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDashboardStatisticHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StatisticResponse> Handle(GetDashboardStatisticRequest request, CancellationToken cancellationToken)
    {
        var from = request.From;
        var to = request.To;
        
        var userCountQuery = await _unitOfWork.AccountRepository.GetAsync(
            predicate: acc => Role.User.Equals(acc.Role) 
                              && (from == null || acc.CreatedAt >= from) 
                              && (to == null || acc.CreatedAt <= to)
        );
        var userCount = await userCountQuery.CountAsync(cancellationToken);
        
        var freelancerCountQuery = await _unitOfWork.AccountRepository.GetAsync(
            predicate: acc => Role.Freelancer.Equals(acc.Role) 
                              && (from == null || acc.CreatedAt >= from) 
                              && (to == null || acc.CreatedAt <= to)
        );
        var freelancerCount =await freelancerCountQuery.CountAsync(cancellationToken);


        var postCountQuery = await _unitOfWork.PostRepository.GetAsync(
            predicate: post => (from == null || post.CreatedAt >= from) 
                               && (to == null || post.CreatedAt <= to));
        var postCount = await postCountQuery.CountAsync(cancellationToken);


        var transactionQuery = await _unitOfWork.TransactionRepository.GetAsync(
            predicate: transaction => TransactionStatus.Completed.Equals(transaction.Status) 
                                      && (from == null || transaction.CreatedAt >= from)
                                      && (to == null || transaction.CreatedAt <= to));

        var revenue = await transactionQuery.SumAsync(transaction => transaction.Amount, cancellationToken);

        return new StatisticResponse()
        {
            UserCount = userCount,
            FreelancerCount = freelancerCount,
            PostCount = postCount,
            Revenue = revenue
        };
    }
}