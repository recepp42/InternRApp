using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.BaseDefinitions;

namespace backend.Domain.Entities;
public class InternShipContentTranslation:ISoftDeletable
{
    public int Id { get; set; }
    public string TitleContent { get; set; }
    public string Description { get; set; }
    public string KnowledgeToDevelop { get; set; }
    public string NeededKnowledge { get; set; }
    public string Location { get; set; }
    public string Comment { get; set; }
    
    public InternShip InternShip { get; set; }
    public int InternShipId { get; set; }
    public Language Language { get; set; }
    public int LanguageId { get; set; }
    //public string CreatorEmail { get; set; }

}
