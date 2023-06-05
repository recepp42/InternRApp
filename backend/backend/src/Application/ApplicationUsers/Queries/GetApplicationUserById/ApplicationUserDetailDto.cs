using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Domain.Entities;
using backend.Domain.Enums;

namespace backend.Application.ApplicationUsers.Queries.GetApplicationUserById;

public class ApplicationUserDetailDto : IMapFrom<ApplicationUser>
{
    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationUser, ApplicationUserDetailDto>();
    }
}