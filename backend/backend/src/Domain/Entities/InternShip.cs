using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.BaseDefinitions;
using backend.Domain.Enums;

namespace backend.Domain.Entities;
public class InternShip:ISoftDeletable
{
    public int Id { get;  set; }
    public Department Unit { get; set; }
    public int UnitId { get; set; }
    public string SchoolYear { get; set; }
    public int MaxStudents  { get; set; }
    public IList<Location> Locations { get; set; }
    public IList<InternShipLocation> InternShipLocations { get; set; }
    public int CurrentCountOfStudents { get; set; }
    public TrainingType RequiredTrainingType { get; set; }
    public IList<InternShipContentTranslation> Translations { get; set; }
    public ApplicationUser? Creator { get; set; }
    public int? CreatorId { get; set; }

}
