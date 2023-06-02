using ClientService.Application.Files.Models;

namespace ClientService.Application.Files.Queries;

public class GetPresignedUrlRequest : IRequest<GetPresignedUrlResponse>
{
    public GetPresignedUrlRequest(string fileName)
    {
        FileName = fileName;
    }

    public string FileName { get; private set; }
}