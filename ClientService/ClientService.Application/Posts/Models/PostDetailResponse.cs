using ClientService.Application.Accounts.Models;
using ClientService.Application.Majors.Models;

namespace ClientService.Application.Posts.Models;

public class PostDetailResponse : PostResponse
{
    public MajorResponse Major { get; set; } = default!;

    public AccountResponse Account { get; set; } = default!;
    
    public AccountResponse? AcceptedAccount { get; set; }

    public AccountResponse? Freelancer { get; set; }
}