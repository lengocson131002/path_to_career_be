using ClientService.Application.Accounts.Models;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers
{
    [ApiController]
    [Route("/api/v1/posts")]
    public class PostController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<PostResponse>> Create(CreatePostRequest request)
        {
            return await Mediator.Send(request);
        }
    }
}
