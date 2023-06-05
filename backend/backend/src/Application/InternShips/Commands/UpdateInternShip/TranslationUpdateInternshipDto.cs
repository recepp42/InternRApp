using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Application.InternShips.Commands.UpdateInternShip;
public class TranslationUpdateInternshipDto
{
    public int? TranslationId { get; set; }
    public int LanguageId { get; set; }
    public string TitleContent { get; set; }
    public string Description { get; set; }
    public string KnowledgeToDevelop { get; set; }
    public string NeededKnowledge { get; set; }
    public string Comment { get; set; }
}
