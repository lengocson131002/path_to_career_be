using ClientService.Application.Files.Models;
using ClientService.Application.Files.Queries;

namespace ClientService.Application.Files.Handlers;

public class DownloadFileHandler : IRequestHandler<DownloadFileRequest, DownloadFileResponse>
{
    private readonly IStorageService _storageService;

    public DownloadFileHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<DownloadFileResponse> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
    {
        var bytes = await _storageService.DownloadFileAsync(request.FileName);
        var contentType = await _storageService.GetContentType(request.FileName);

        return new DownloadFileResponse(bytes, contentType);
    }
}