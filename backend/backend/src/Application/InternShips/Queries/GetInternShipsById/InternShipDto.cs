using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Commands.CreateInternShip;
using backend.Application.InternShips.Common;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Entities;
using backend.Domain.Enums;

namespace backend.Application.InternShips.Queries.GetInternShipById;

public class InternShipDto:IMapFrom<InternShip>
{
    public string SchoolYear { get; set; }
    public int InternShipId { get; set; }
    public int UnitId{ get; set; }
    public int MaxCountOfStudents { get; set; }
    public int CurrentCountOfStudents { get; set; }
    public TrainingType TrainingType { get; set; }

    public IList<LocationDto> Locations { get; set; }
    public IList<TranslationDto> Versions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InternShip, InternShipDto>()
                    .ForMember(dest => dest.InternShipId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.MaxCountOfStudents, opt => opt.MapFrom(src => src.MaxStudents))
                    .ForMember(dest => dest.Versions, opt => opt.MapFrom(src => src.Translations))
                    .ForMember(dest => dest.TrainingType, opt => opt.MapFrom(src => src.RequiredTrainingType))
                    .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId));
                    


    }
}