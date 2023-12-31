using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Majors.Models;
using ClientService.Application.Messages.Models;
using ClientService.Application.Notifications.Models;
using ClientService.Application.Reviews.Models;
using ClientService.Application.Services.Commands;
using ClientService.Application.Services.Models;
using ClientService.Application.Transactions.Models;
using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Account
        CreateMap<Account, AccountResponse>();
        CreateMap<Account, AccountDetailResponse>()
            .ForMember(des => des.Score, 
                opt => opt.MapFrom(
                    src => src.Reviews != null && src.Reviews.Any() ? src.Reviews.Sum(r => r.Score) / src.Reviews.Count : 0));
        
        CreateMap<RegisterAccountRequest, Account>();
        
        // Major
        CreateMap<Major, MajorResponse>();
        
        // Review
        CreateMap<Review, ReviewResponse>();

        // Service
        CreateMap<Service, ServiceResponse>();
        CreateMap<Service, ServiceDetailResponse>();
        CreateMap<CreateServiceRequest, Service>();
        
        // Registration
        CreateMap<AccountService, RegistrationResponse>();
        
        // Message 
        CreateMap<Message, MessageResponse>();
        
        // Transaction
        CreateMap<Transaction, TransactionResponse>()
            .ForMember(des => des.PostId,
                opt => opt.MapFrom(src => src.Post.Id));
        CreateMap<Transaction, TransactionDetailResponse>();
        
        // Notification
        CreateMap<Notification, NotificationResponse>();
    }
}