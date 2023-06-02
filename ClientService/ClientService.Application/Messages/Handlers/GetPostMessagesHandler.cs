using AutoMapper;
using ClientService.Application.Messages.Models;
using ClientService.Application.Messages.Queries;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Application.Messages.Handlers;

public class GetPostMessagesHandler : IRequestHandler<GetPostMessagesRequest, ListResponse<MessageResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPostMessagesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ListResponse<MessageResponse>> Handle(GetPostMessagesRequest request, CancellationToken cancellationToken)
    {
        var query = await _unitOfWork.MessageRepository.GetAsync(
                predicate: message => message.PostId == request.PostId && message.DeletedAt == null,
                orderBy: query => query.OrderBy(message => message.CreatedAt)
        );
        
        var messages = await query.ToListAsync(cancellationToken);

        return new ListResponse<MessageResponse>(_mapper.Map<List<MessageResponse>>(messages));
    }
}