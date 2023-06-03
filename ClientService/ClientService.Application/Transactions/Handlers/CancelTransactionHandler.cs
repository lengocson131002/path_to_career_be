using System.Linq.Expressions;
using ClientService.Application.Transactions.Commands;
using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Transactions.Handlers;

public class CancelTransactionHandler : IRequestHandler<CancelTransactionRequest, StatusResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CancelTransactionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StatusResponse> Handle(CancelTransactionRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.TransactionRepository.GetAsync(
            predicate: transaction => transaction.Id == request.Id,
            includes: new ListResponse<Expression<Func<Transaction, object>>>()
            {
                transaction => transaction.Post
            });

        var transaction = await query.FirstOrDefaultAsync(cancellationToken);
        if (transaction == null || !TransactionStatus.New.Equals(transaction.Status))
        {
            throw new ApiException(ResponseCode.TransactionNotFound);
        }

        transaction.Status = TransactionStatus.Canceled;
        await _unitOfWork.TransactionRepository.UpdateAsync(transaction);
        
        var post = transaction.Post;
        post.Status = PostStatus.New;
        post.Transaction = null;
        
        await _unitOfWork.PostRepository.UpdateAsync(post);
        await _unitOfWork.SaveChangesAsync();
        
        return new StatusResponse(true);
    }
}