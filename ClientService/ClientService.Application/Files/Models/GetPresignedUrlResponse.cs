namespace ClientService.Application.Files.Models;

public class GetPresignedUrlResponse
{
    public GetPresignedUrlResponse(string url)
    {
        Url = url;
    }

    public string Url { get; private set; }
}