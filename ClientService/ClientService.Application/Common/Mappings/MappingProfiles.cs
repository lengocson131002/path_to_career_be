using AutoMapper;
using ClientService.Application.Accounts.Commands;
using ClientService.Application.Accounts.Models;
using ClientService.Application.Majors.Models;
using ClientService.Domain.Entities;

namespace ClientService.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Account
        CreateMap<Account, AccountResponse>();
        CreateMap<Account, AccountDetailResponse>();
        CreateMap<RegisterAccountRequest, Account>();
        
        // Major
        CreateMap<Major, MajorResponse>();
    }
}