using ClientService.Application.Files.Models;
using MediatR;

namespace ClientService.Application.Files.Queries;

public class DownloadFileRequest : IRequest<DownloadFileResponse>
{
    public DownloadFileRequest(string fileName)
    {
        FileName = fileName;
    }

    public string FileName { get; private set; }
}