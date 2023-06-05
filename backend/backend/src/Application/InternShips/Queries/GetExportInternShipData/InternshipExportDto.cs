using AutoMapper;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Application.InternShips.Queries.GetAllInternShips;
using backend.Application.InternShips.Queries.GetInternShipById;
using backend.Domain.Entities;
using backend.Domain.Enums;

namespace backend.Application.InternShips.Queries.GetExportInternShipData;

public class InternShipExportDto
{
    public string SchoolYear { get; set; }
    public TrainingType TrainingType { get; set; }
    public IList<LocationDto> Locations { get; set; } 
    public TranslationDto Translation { get; set; }
    public IList<LanguageListDto> Languages { get; set; } //new
}