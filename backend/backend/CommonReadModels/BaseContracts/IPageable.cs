namespace CommonReadModels.BaseContracts;

public interface IPageable
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}