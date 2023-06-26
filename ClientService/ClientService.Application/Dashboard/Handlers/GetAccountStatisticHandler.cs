using ClientService.Application.Common.Persistence;
using ClientService.Application.Dashboard.Models;
using ClientService.Application.Dashboard.Queries;
using LinqKit;

namespace ClientService.Application.Dashboard.Handlers;

public class GetAccountStatisticHandler : IRequestHandler<GetAccountStatisticRequest, AccountStatisticResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAccountStatisticHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountStatisticResponse> Handle(GetAccountStatisticRequest request, CancellationToken cancellationToken)
    {
        var response = new AccountStatisticResponse();

        // get all role
        var roleQuery = await _unitOfWork.AccountRepository.GetAsync();
        var roles = roleQuery.Select(acc => acc.Role).Distinct().ToList();

        foreach (var role in roles)
        {
            var accountQuery = await _unitOfWork.AccountRepository.GetAsync(
                predicate: acc => role.Equals(acc.Role) 
                                  && (request.From == null || acc.CreatedAt >= request.From)
                                  && (request.To == null || acc.CreatedAt <= request.To));

            var statusDict = accountQuery
                .GroupBy(acc => acc.IsAccepted)
                .Select(g => new
                {
                    IsAccepted = g.Key,
                    Count = g.Count()
                }).ToDictionary(item => item.IsAccepted);

            var statistic = new AccountStatistic()
            {
                Active = statusDict.TryGetValue(true, out var aCount) ? aCount.Count : 0,
                Inactive = statusDict.TryGetValue(false, out var iCount) ? iCount.Count : 0
            };
            
            response.Add(role, statistic);
        }

        return response;
    }
}