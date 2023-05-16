using ClientService.Application.Files.Commands;
using ClientService.Application.Files.Models;
using ClientService.Application.Files.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/files")]
public class FileController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UploadFileResponse>> UploadFile(IFormFile file)
    {
        return await Mediator.Send(new UploadFileRequest()
        {
            File = file
        });
    }
    
    [HttpGet("{fileName}")]
    public async Task<IActionResult> DownloadFile([FromRoute] string fileName)
    {
        var response = await Mediator.Send(new DownloadFileRequest(fileName));
        return File(response.Data, response.ContentType);
    }

    [HttpGet("{fileName}/presigned-url")] 
    public async Task<ActionResult<GetPresignedUrlResponse>> GetPresignedUrl([FromRoute] string fileName)
    {
        return await Mediator.Send(new GetPresignedUrlRequest(fileName));
    }
}