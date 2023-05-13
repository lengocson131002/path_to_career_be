using AutoMapper;
using IdentityService.Application.Accounts.Commands;
using IdentityService.Application.Accounts.Models;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Account, AccountResponse>();
        CreateMap<RegisterAccountRequest, Account>();
    }
}