using ClientService.Application.Posts.Models;
using MediatR;

namespace ClientService.Application.Posts.Commands;

public class FreelancerCancelApplicationRequest : IRequest<PostApplicationResponse>
{
    public long PostId { get; set; }
}