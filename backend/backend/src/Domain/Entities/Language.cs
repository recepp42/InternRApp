using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.BaseDefinitions;

namespace backend.Domain.Entities;
public class Language:ISoftDeletable
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public IList<InternShipContentTranslation> InternshipTranslations{ get; set; }
    public IList<PrefaceTranslation> PrefaceTranslations { get; set; }
    public int? CreatorId { get; set; }
    public ApplicationUser? Creator { get; set; }


}
