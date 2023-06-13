using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Runtime.Internal;
using AutoMapper;
using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Exceptions;
using ClientService.Application.Common.Interfaces;
using ClientService.Application.Messages.Models;
using ClientService.Domain.Entities;
using ClientService.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientService.Infrastructure.SignalR;

public class ChatHub : Hub
{
    private const string ReceiveMessageMethod = "ReceiveMessage";

    private const string PostChatRoomPrefix = "post_";
    private readonly ChatConnectionManager _chatConnectionManager;

    private readonly ILogger<ChatHub> _logger;

    private readonly IMapper _mapper;

    private readonly INotificationService _notificationService;

    private readonly IUnitOfWork _unitOfWork;


    public ChatHub(ChatConnectionManager chatConnectionManager, IUnitOfWork unitOfWork, ILogger<ChatHub> logger,
        IMapper mapper, INotificationService notificationService)
    {
        _chatConnectionManager = chatConnectionManager;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task JoinRoom(UserConnection connection)
    {
        // Validate connection
        await ValidateConnection(connection);

        var roomName = GetRoomName(connection);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        _chatConnectionManager.Connections[Context.ConnectionId] = connection;
    }

    public async Task SendMessage(ChatMessage message)
    {
        if (!_chatConnectionManager.Connections.TryGetValue(Context.ConnectionId, out var connection)) return;

        // Save message
        var messageEntity = new Message
        {
            Type = message.Type,
            Content = message.Content,
            AccountId = connection.AccountId,
            PostId = connection.PostId
        };

        await _unitOfWork.MessageRepository.AddAsync(messageEntity);
        await _unitOfWork.SaveChangesAsync();

        // push notification when partner is not in the post chat
        var partnerId = connection.PartnerId;
        if (partnerId != null && _chatConnectionManager.Connections.Values.All(con => con.AccountId != partnerId))
        {
            var notification = new Notification(NotificationType.MessageCreated)
            {
                AccountId = connection.PartnerId,
                Data = JsonSerializer.Serialize(messageEntity, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }),
                ReferenceId = connection.PostId.ToString()
            };
            await _notificationService.PushNotification(notification);
        }

        var roomName = GetRoomName(connection);
        await Clients.Groups(roomName).SendAsync(ReceiveMessageMethod, _mapper.Map<MessageResponse>(messageEntity));
    }

    private async Task ValidateConnection(UserConnection userConnection)
    {
        var postQuery = await _unitOfWork.PostRepository.GetAsync(
            post => post.Id == userConnection.PostId,
            includes: new AutoConstructedList<Expression<Func<Post, object>>>
            {
                post => post.Account,
                post => post.Freelancer
            }
        );
        var post = await postQuery.FirstOrDefaultAsync();

        if (post == null
            || post.Freelancer == null
            || (post.Account.Id != userConnection.AccountId && post.Freelancer.Id != userConnection.AccountId))
        {
            throw new ApiException(ResponseCode.PostNotFound);
        }

        if (!PostStatus.Accepted.Equals(post.Status))
        {
            throw new ApiException(ResponseCode.InvalidPostStatus);
        }

        userConnection.PartnerId = userConnection.AccountId == post.AccountId ? post.FreelancerId : post.AccountId;
    }

    public async Task Leave(long postId)
    {
        if (_chatConnectionManager.Connections.TryGetValue(Context.ConnectionId, out var connection))
            _chatConnectionManager.Connections.Remove(Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetRoomName(postId));
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_chatConnectionManager.Connections.TryGetValue(Context.ConnectionId, out var connection))
            _chatConnectionManager.Connections.Remove(Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }

    private string GetRoomName(UserConnection connection)
    {
        return GetRoomName(connection.PostId);
    }

    private string GetRoomName(long postId)
    {
        return $"{PostChatRoomPrefix}{postId}";
    }
}