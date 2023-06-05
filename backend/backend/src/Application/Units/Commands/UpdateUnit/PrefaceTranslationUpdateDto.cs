using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Application.Units.Queries.GetAllUnitsWithMinimalInfo;
using backend.Domain.Entities;

namespace backend.Application.Units.Commands.UpdateUnit;
public class PrefaceTranslationUpdateDto:IMapFrom<PrefaceTranslation>
{
    public int? TranslationId { get; set; }
    public string Content { get; set; }
    public int LanguageId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, PrefaceTranslationUpdateDto>()
            .ForMember(x => x.TranslationId, dest => dest.MapFrom(src => src.Id));

    }
}
