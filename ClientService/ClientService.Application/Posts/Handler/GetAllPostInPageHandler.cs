using System.Linq.Expressions;
using AutoMapper;
using ClientService.Application.Posts.Models;
using ClientService.Application.Posts.Queries;
using ClientService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ClientService.Application.Posts.Handler;

public class GetAllPostInPageHandler : IRequestHandler<GetAllPostInPageRequest, PaginationResponse<Post, PostResponse>>
{
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ILogger<GetAllPostInPageHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPostInPageHandler(IUnitOfWork unitOfWork,
        ILogger<GetAllPostInPageHandler> logger,
        IMapper mapper,
        ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }

    public async Task<PaginationResponse<Post, PostResponse>> Handle(GetAllPostInPageRequest request,
        CancellationToken cancellationToken)
    {
        var currentAccount = await _currentAccountService.GetCurrentAccount();
        if (Role.User.Equals(currentAccount.Role) &&
            (request.AccountId == null || currentAccount.Id != request.AccountId))
        {
            throw new ApiException(ResponseCode.InvalidQuery);
        }

        if (request.FreelancerId != null && currentAccount.Id != request.FreelancerId)
        {
            throw new ApiException(ResponseCode.InvalidQuery);
        }

        var posts = await _unitOfWork.PostRepository.GetAsync(
            predicate: request.GetExpressions(),
            orderBy: request.GetOrder(),
            includes: new ListResponse<Expression<Func<Post, object>>>()
            {
                post => post.Major,
                post => post.Account,
                post => post.Freelancer 
            });

        return new PaginationResponse<Post, PostResponse>(posts, request.PageNumber, request.PageSize,
            entity => _mapper.Map<PostResponse>(entity));
    }
}