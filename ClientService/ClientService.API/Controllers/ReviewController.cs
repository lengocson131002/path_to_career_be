using ClientService.Application.Common.Models.Response;
using ClientService.Application.Reviews.Commands;
using ClientService.Application.Reviews.Models;
using ClientService.Application.Reviews.Queries;
using ClientService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/reviews")]
public class ReviewController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginationResponse<Review, ReviewResponse>>> GetAllReviews([FromQuery] GetAllReviewsRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPost]
    public async Task<ActionResult<ReviewResponse>> CreateReview([FromBody] CreateReviewRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ReviewResponse>> UpdateReview([FromRoute] long id, [FromBody] UpdateReviewRequest request)
    {
        request.Id = id;
        return await Mediator.Send(request);
    }
    
     
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReviewResponse>> RemoveReview([FromRoute] long id)
    {
        
        return await Mediator.Send(new RemoveReviewRequest()
        {
            Id = id
        });
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewResponse>> GetReview([FromRoute] long id)
    {
        
        return await Mediator.Send(new GetReviewRequest()
        {
            Id = id
        });
    }
}