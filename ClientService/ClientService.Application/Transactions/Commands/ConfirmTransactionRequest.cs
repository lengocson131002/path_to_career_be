namespace ClientService.Application.Transactions.Commands;

public class ConfirmTransactionRequest : IRequest<StatusResponse>
{
    public ConfirmTransactionRequest(long id)
    {
        Id = id;
    }

    public long Id { get; private set; }
}