using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.InternShips.Commands.CreateInternShip;
using backend.Application.InternShips.Common;
using backend.Domain.Enums;

namespace backend.Application.InternShips.Commands.UpdateInternShip;
public class InternShipUpdateDto
{
    public int InternShipId { get; set; }
    public string SchoolYear { get; set; }
    public int UnitId { get; set; }
    public byte MaxCountOfStudents { get; set; }
    public TrainingType TrainingType { get; set; }
    public byte CurrentCountOfStudents { get; set; }


    public IList<LocationDto> Locations { get; set; }
    public IList<TranslationUpdateInternshipDto> Versions { get; set; }
}
