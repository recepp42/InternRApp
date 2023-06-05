using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Entities;
using backend.Domain.Enums;

namespace backend.Application.InternShips.Commands.CreateInternShip;

public class InternShipCreateDto
{
    public string SchoolYear { get; set; }
    public int UnitId { get; set; }
    public int MaxCountOfStudents { get; set; }
    public int CurrentCountOfStudents { get; set; }
    public TrainingType TrainingType { get; set; }
   

    public IList<LocationDto> Locations { get; set; }
    public IList<TranslationCreateInternshipDto> Versions { get; set; }


}