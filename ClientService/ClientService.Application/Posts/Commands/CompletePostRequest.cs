namespace ClientService.Application.Posts.Commands;

public class CompletePostRequest : IRequest<StatusResponse>
{
    public long PostId { get; private set; }

    public CompletePostRequest(long postId)
    {
        this.PostId = postId;
    }
}