using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.BaseDefinitions;

namespace backend.Domain.Entities;
public class PrefaceTranslation:ISoftDeletable
{
    public int Id { get; set; }
    public string Content { get; set; }
    public Language Language { get; set; }
    public Department Unit { get; set; }
    public int LanguageId { get; set; }
    public int UnitId{ get; set; }
}
