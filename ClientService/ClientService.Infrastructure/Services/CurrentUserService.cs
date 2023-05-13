namespace ClientService.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public string Principal => "anonymous";
}