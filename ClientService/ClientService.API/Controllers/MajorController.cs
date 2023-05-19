using ClientService.Application.Common.Models.Response;
using ClientService.Application.Majors.Models;
using ClientService.Application.Majors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/majors")]
public class MajorController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<ListResponse<MajorResponse>>> GetAllResponse([FromQuery] GetAllMajorsRequest request)
    {
        return await Mediator.Send(request);
    }
}