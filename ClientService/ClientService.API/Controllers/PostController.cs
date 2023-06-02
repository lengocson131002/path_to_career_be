using ClientService.Application.Common.Models.Response;
using ClientService.Application.Messages.Models;
using ClientService.Application.Messages.Queries;
using ClientService.Application.Posts.Commands;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
using ClientService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers
{
    [ApiController]
    [Route("/api/v1/posts")]
    public class PostController : ApiControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostResponse>> Create(CreatePostRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("{postId}/applications")]
        [Authorize(Roles = "Freelancer")]
        public async Task<ActionResult<ApplyPostResponse>> Apply([FromRoute] long postId, [FromBody] ApplyPostRequest request)
        {
            request.PostId = postId;
            return await Mediator.Send(request);
        }

        [HttpGet("{postId}/applications")]
        [Authorize]
        public async Task<ActionResult<PostApplicationListResponse>> GetAllApplication([FromRoute] long postId)
            
        {
            GetAllApplicationRequest request = new GetAllApplicationRequest();
            request.PostId = postId;
            return await Mediator.Send(request);
        }
        
        [HttpDelete("{postId}/applications")]
        [Authorize(Roles = "Freelancer")]
        public async Task<ActionResult<PostApplicationResponse>> CancelApplication([FromRoute] long postId)
            
        {
            var request = new FreelancerCancelApplicationRequest();
            request.PostId = postId;
            return await Mediator.Send(request);
        }

        [HttpGet("{postId}/applications/{applicationId}")]
        [Authorize]
        public async Task<ActionResult<PostApplicationResponse>> GetDetail([FromRoute] long postId, [FromRoute] long applicationId)
        {
            GetDetailPostApplicationRequest request = new GetDetailPostApplicationRequest();
            request.ApplicationId = applicationId;
            request.PostId = postId;
            return await Mediator.Send(request);
        }

        [HttpPost("{postId}/applications/{applicationId}/accept")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<StatusResponse>> Accept([FromRoute] long postId, [FromRoute] long applicationId)
        {
            AcceptApplicationRequest request = new AcceptApplicationRequest();
            request.PostId = postId;
            request.ApplicationId = applicationId;
            return await Mediator.Send(request);
        }

        [HttpDelete("{postId}/applications/{applicationId}/cancel")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostApplicationResponse>> CancelApplication([FromRoute] long postId, [FromRoute] long applicationId)
        {
            CancelApplicationRequest request = new CancelApplicationRequest();
            request.PostId = postId;
            request.ApplicationId = applicationId;
            return await Mediator.Send(request);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<PostResponse>> Update([FromRoute] long id, [FromBody] UpdatePostRequest request)
        {
            request.Id = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<StatusResponse>> Delete([FromRoute] long id)
        {
            DeletePostRequest request = new DeletePostRequest();
            request.Id = id;
            return await Mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDetailResponse>> GetDetail([FromRoute] long id)
        {
            GetDetailPostRequest request = new GetDetailPostRequest();
            request.Id = id;
            return await Mediator.Send(request);
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<Post, PostResponse>>> GetAllInPage([FromQuery] GetAllPostInPageRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("{id:int}/accept")]
        [Authorize(Roles = "Freelancer")]
        public async Task<ActionResult<StatusResponse>> AcceptPost([FromRoute] long id)
        {
            var request = new AcceptPostRequest(id);
            return await Mediator.Send(request);
        }
        
        [HttpPost("{id:int}/complete")]
        [Authorize(Roles = "Freelancer")]
        public async Task<ActionResult<StatusResponse>> CompletePost([FromRoute] long id)
        {
            var request = new CompletePostRequest(id);
            return await Mediator.Send(request);
        }
        
        [HttpGet("{id:int}/messages")]
        [Authorize]
        public async Task<ActionResult<ListResponse<MessageResponse>>> GetPostMessages([FromRoute] long id)
        {
            var request = new GetPostMessagesRequest(id);
            return await Mediator.Send(request);
        }
    }
}
