using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Paging;

namespace backend.Application.InternShips.Queries.getFilteredInternShip;
public class InternShipFilteredDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public List<int> UnitIds { get; set; }
    public List<string> SchoolYear { get; set; }
    public List<int> LanguageIds { get; set; }
}
