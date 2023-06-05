using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.Languages.Queries.GetLanguageById;
using backend.Domain.Entities;
using backend.Domain.Enums;

namespace backend.Application.ApplicationUsers.Queries.GetUserByUsernameAndPassword;
public class UserDto:IMapFrom<ApplicationUser>
{
    public int Id { get; set; }
    public string Email { get; set; }
    public Role Role{ get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationUser, UserDto>();

    }
}
