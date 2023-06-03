namespace ClientService.Application.Transactions.Commands;

public class CancelTransactionRequest : IRequest<StatusResponse>
{
    public CancelTransactionRequest(long id)
    {
        Id = id;
    }

    public long Id { get; private set; }
}