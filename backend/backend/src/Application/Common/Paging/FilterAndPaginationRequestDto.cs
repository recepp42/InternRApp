using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Application.Common.Paging;
public class FilterAndPaginationRequestDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string UnitName { get; set; }
}
