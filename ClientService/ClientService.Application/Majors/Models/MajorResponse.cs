namespace ClientService.Application.Majors.Models;

public class MajorResponse
{
    public long Id { get; set; }
    
    public string Name { get; set; } = default!;

    public string Code { get; set; } = default!;
}