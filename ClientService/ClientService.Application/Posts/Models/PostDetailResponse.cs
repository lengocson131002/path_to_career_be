using ClientService.Application.Reviews.Models;
using ClientService.Application.Transactions.Models;

namespace ClientService.Application.Posts.Models;

public class PostDetailResponse : PostResponse
{
    public TransactionResponse? Transaction { get; set; }
    
    public ReviewResponse? Review { get; set; }
}