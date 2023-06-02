using ClientService.Application.Majors.Models;

namespace ClientService.Application.Majors.Queries;

public class GetAllMajorsRequest : IRequest<ListResponse<MajorResponse>>
{
    private string? _query;
    
    public string? Query
    {
        get => _query;
        set => _query = value?.ToLower().Trim();
    }
}