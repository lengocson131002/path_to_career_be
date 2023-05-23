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

        [HttpPost("application")]
        public async Task<ActionResult<ApplyPostResponse>> Apply(ApplyPostRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<PostResponse>> Update(UpdatePostRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeletePostResponse>> Delete([FromRoute] long id)
        {
            DeletePostRequest request = new DeletePostRequest();
            request.Id = id;
            return await Mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> GetDetail([FromRoute] long id)
        {
            GetDetailPostRequest request = new GetDetailPostRequest();
            request.Id = id;
            return await Mediator.Send(request);
        }
    }
}
