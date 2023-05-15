using ClientService.Application.Common.Interfaces;
using ClientService.Application.Files.Models;
using ClientService.Application.Files.Queries;
using MediatR;

namespace ClientService.Application.Files.Handlers;

public class GetPresignedUrlHandler : IRequestHandler<GetPresignedUrlRequest, GetPresignedUrlResponse>
{
    private readonly IStorageService _storageService;

    public GetPresignedUrlHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<GetPresignedUrlResponse> Handle(GetPresignedUrlRequest request, CancellationToken cancellationToken)
    {
        var url = await _storageService.GetPresignedUrlAsync(request.FileName);
        return new GetPresignedUrlResponse(url);
    }
}