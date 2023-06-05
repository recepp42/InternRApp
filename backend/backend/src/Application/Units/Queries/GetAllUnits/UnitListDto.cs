using System.Text.Json.Serialization;
using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.Units.Commands.UpdateUnit;
using backend.Application.Units.Queries.GetUnitById;
using backend.Domain.Entities;

namespace backend.Application.Units.Queries.GetAllUnits;

public class UnitListDto : IMapFrom<Department>
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> ManagerEmails { get; set; }
    //public IList<PrefaceTranslationUpdateDto> PrefaceTranslations{ get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, UnitListDto>();
            //.ForMember(x => x.PrefaceTranslations, dest => dest.MapFrom(src => src.PrefaceTranslations));

    }
}