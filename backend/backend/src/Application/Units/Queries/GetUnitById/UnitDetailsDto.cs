using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Domain.Entities;

namespace backend.Application.Units.Queries.GetUnitById;

public class UnitDetailsDto:IMapFrom<Department>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> ManagerEmails { get; set; }
    public IList<PrefaceTranslationDto> PrefaceTranslations { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, UnitDetailsDto>()
            .ForMember(x => x.PrefaceTranslations, dest => dest.MapFrom(src => src.PrefaceTranslations));
    }
}