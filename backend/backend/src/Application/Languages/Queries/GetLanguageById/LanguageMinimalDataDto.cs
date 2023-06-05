using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Domain.Entities;

namespace backend.Application.Languages.Queries.GetLanguageById;
public class LanguageMinimalDataDto:IMapFrom<Language>
{
    public string Code { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Language,LanguageMinimalDataDto>();
    }
}
