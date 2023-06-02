using ClientService.Application.Files.Commands;
using ClientService.Application.Files.Models;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Files.Handlers;

public class UploadFileHandler : IRequestHandler<UploadFileRequest, UploadFileResponse>
{
    private readonly IStorageService _storageService;
    private readonly ILogger<UploadFileHandler> _logger;

    public UploadFileHandler(IStorageService storageService, ILogger<UploadFileHandler> logger)
    {
        _storageService = storageService;
        _logger = logger;
    }

    public async Task<UploadFileResponse> Handle(UploadFileRequest request, CancellationToken cancellationToken)
    {
        var uploadedFileName = await _storageService.UploadFileAsync(request.File);
        
        return new UploadFileResponse(uploadedFileName, _storageService.GetObjectUrl(uploadedFileName));
    }
}