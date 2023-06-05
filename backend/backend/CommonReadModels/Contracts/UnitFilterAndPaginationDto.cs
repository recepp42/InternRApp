
using CommonReadModels.BaseContracts;

namespace CommonReadModels.Contracts;
public class UnitFilterAndPaginationDto : IPageable
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string UnitName { get; set; }
}
