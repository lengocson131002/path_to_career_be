namespace ClientService.Domain.Entities;

public class AccountMajor
{
    public long AccountId { get; set; }

    public Account Account { get; set; } = default!;
    
    public long MajorId { get; set; }

    public Major Major { get; set; } = default!;
}