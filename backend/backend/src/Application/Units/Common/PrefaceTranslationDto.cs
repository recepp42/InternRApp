using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Domain.Entities;

namespace backend.Application.Units.Common;
public class PrefaceTranslationDto:IMapFrom<PrefaceTranslation>
{
    public int TranslationId { get; set; }
    public string Content { get; set; }
    public LanguageListDto Language { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PrefaceTranslation, PrefaceTranslationDto>()
        .ForMember(dest => dest.Language, opt => opt.MapFrom(x => x.Language))
        .ForMember(dest => dest.TranslationId, opt => opt.MapFrom(src => src.Id));

    }
}
