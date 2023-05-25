using ClientService.Application.Accounts.Models;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
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

        [HttpPost("{postId}/applications")]
        public async Task<ActionResult<ApplyPostResponse>> Apply([FromRoute] long postId, [FromBody] ApplyPostRequest request)
        {
            request.PostId = postId;
            return await Mediator.Send(request);
        }

        [HttpGet("{PostId}/applications")]
        public async Task<ActionResult<PostApplicationPageResponse>> GetAllInPage([FromQuery] GetAllApplicationRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("{postId}/applications/{applicationId}")]
        public async Task<ActionResult<PostApplicationResponse>> GetDetail([FromRoute] long postId, [FromRoute] long applicationId)
        {
            GetDetailPostApplicationRequest request = new GetDetailPostApplicationRequest();
            request.ApplicationId = applicationId;
            request.PostId = postId;
            return await Mediator.Send(request);
        }

        [HttpPost("{postId}/applications/{applicationId}/accept")]
        public async Task<ActionResult<StatusResponse>> Accept([FromRoute] long postId, [FromRoute] long applicationId)
        {
            AcceptApplicationRequest request = new AcceptApplicationRequest();
            request.PostId = postId;
            request.ApplicationId = applicationId;
            return await Mediator.Send(request);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<PostResponse>> Update(UpdatePostRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StatusResponse>> Delete([FromRoute] long id)
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
