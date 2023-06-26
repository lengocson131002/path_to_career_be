using ClientService.Application.Common.Persistence;
using ClientService.Application.Dashboard.Models;
using ClientService.Application.Dashboard.Queries;

namespace ClientService.Application.Dashboard.Handlers;

public class GetPostStatisticHandler : IRequestHandler<GetPostStatisticRequest, PostStatisticResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPostStatisticHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PostStatisticResponse> Handle(GetPostStatisticRequest request, CancellationToken cancellationToken)
    {
        var response = new PostStatisticResponse();

        var from = request.From;
        var to = request.To;
        
        // count by status
        var postQuery = await _unitOfWork.PostRepository.GetAsync(
            predicate: post => (from == null || post.CreatedAt >= from) && (to == null || post.CreatedAt <= to));

        var statusDict = postQuery
            .GroupBy(post => post.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count()
            }).ToDictionary(item => item.Status);

        var statusStatistic = new Dictionary<PostStatus, int>();
        foreach (var status in Enum.GetValues<PostStatus>())
        {
            var count = statusDict.TryGetValue(status, out var c) ? c.Count : 0;
            statusStatistic.Add(status, count);
        }

        response.ByStatus = statusStatistic;
            
        // count by service type
        var serviceTypeDict = postQuery
            .GroupBy(post => post.ServiceType)
            .Select(g => new
            {
                ServiceType = g.Key,
                Count = g.Count()
            }).ToDictionary(item => item.ServiceType);

        var serviceTypeStatistic = new Dictionary<ServiceType, int>();
        foreach (var serviceType in Enum.GetValues<ServiceType>())
        {
            var count = serviceTypeDict.TryGetValue(serviceType, out var c) ? c.Count : 0;
            serviceTypeStatistic.Add(serviceType, count);
        }

        response.ByService = serviceTypeStatistic;
        
        return response;
    }
}