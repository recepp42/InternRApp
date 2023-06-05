using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Entities;

namespace backend.Application.Units.Commands.UpdateUnit;
public class UnitListUpdateDto : IMapFrom<Department>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> ManagerEmails { get; set; }
    public IList<PrefaceTranslationUpdateDto> PrefaceTranslations { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, UnitListUpdateDto>()
        .ForMember(x => x.PrefaceTranslations, dest => dest.MapFrom(src => src.PrefaceTranslations));

    }
}
