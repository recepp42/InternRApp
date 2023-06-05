using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Application.InternShips.Queries.GetExportInternShipData;
public class InternshipExportRequestDto
{
    public List<int> UnitIds { get; set; }
    public string SchoolYear { get; set; }
    public int LanguageId { get; set; }

}
