namespace ClientService.Infrastructure.Services;

public interface ICurrentUserService
{
    public string Principal { get; }
}