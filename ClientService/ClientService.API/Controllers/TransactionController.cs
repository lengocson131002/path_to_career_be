using Amazon.Runtime;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Models.Response;
using ClientService.Application.Transactions.Commands;
using ClientService.Application.Transactions.Models;
using ClientService.Application.Transactions.Queries;
using ClientService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers;

[ApiController]
[Route("/api/v1/transactions")]
[Authorize]
public class TransactionController : ApiControllerBase
{
    [HttpGet("")]
    public async Task<PaginationResponse<Transaction, TransactionResponse>> GetAllTransaction([FromQuery] GetAllTransactionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SortColumn))
        {
            request.SortColumn = "CreatedAt";
            request.SortDir = SortDirection.Desc;
        }
        return await Mediator.Send(request);
    }
    
    [HttpGet("{id:long}")]
    public async Task<TransactionDetailResponse> GetTransactionDetail([FromRoute] long id)
    {
        var request = new GetTransactionDetailRequest(id);
        return await Mediator.Send(request);
    }
    
    [HttpPost("{id:int}/confirm")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<StatusResponse>> ConfirmTransaction([FromRoute] long id)
    {
        var request = new ConfirmTransactionRequest(id);
        return await Mediator.Send(request);
    }
    
    [HttpDelete("{id:int}/cancel")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<StatusResponse>> CancelTransaction([FromRoute] long id)
    {
        var request = new CancelTransactionRequest(id);
        return await Mediator.Send(request);
    }
}