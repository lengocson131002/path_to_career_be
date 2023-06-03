using ClientService.Application.Transactions.Models;

namespace ClientService.Application.Transactions.Queries;

public class GetTransactionDetailRequest : IRequest<TransactionDetailResponse>
{
    public GetTransactionDetailRequest(long id)
    {
        Id = id;
    }

    public long Id { get; private set; }
}