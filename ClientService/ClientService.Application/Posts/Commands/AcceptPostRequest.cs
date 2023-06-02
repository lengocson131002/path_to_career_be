namespace ClientService.Application.Posts.Commands;

public class AcceptPostRequest : IRequest<StatusResponse>
{
    public AcceptPostRequest(long postId)
    {
        PostId = postId;
    }

    public long PostId { get; private set; }
}