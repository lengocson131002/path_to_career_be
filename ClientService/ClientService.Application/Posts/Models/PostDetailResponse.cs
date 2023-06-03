using ClientService.Application.Transactions.Models;

namespace ClientService.Application.Posts.Models;

public class PostDetailResponse : PostResponse
{
    public TransactionResponse? Transaction { get; set; }
}