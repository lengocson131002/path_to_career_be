using ClientService.Application.Messages.Models;

namespace ClientService.Application.Messages.Queries;

public class GetPostMessagesRequest : IRequest<ListResponse<MessageResponse>>
{
    public GetPostMessagesRequest(long postId)
    {
        PostId = postId;
    }

    public long PostId { get; private set; }
    
}