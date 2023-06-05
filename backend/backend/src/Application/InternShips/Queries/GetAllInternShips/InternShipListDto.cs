using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Queries.GetInternShipById;
using backend.Domain.Entities;

namespace backend.Application.InternShips.Queries.GetAllInternShips;

public class InternShipListDto : IMapFrom<InternShip>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string UnitName { get; set; }
    public int MaxStudents { get; set; }
    public int CurrentCountOfStudents { get; set; }
    public int? CreatorId{ get; set; }

    //public IList<TranslationDto> Translations { get; set; }

    public void Mapping(Profile profile)
    {

        profile.CreateMap<InternShip, InternShipListDto>()
            .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name))
            .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Translations.FirstOrDefault().TitleContent));


    }


}

