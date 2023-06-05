using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.Common.Interfaces;
using backend.Application.Common.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Units.Queries.GetAllUnits;
public class GetAllQuery : IRequest<PagedList<UnitListDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string UnitName { get; set; }
}
public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PagedList<UnitListDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private IMapper _iMapper;

    public GetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _iMapper = mapper;
    }

    public async Task<PagedList<UnitListDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        
        var queryable = _dbContext.Departments.AsQueryable();
        if (request.UnitName!=null&&request.UnitName != "")
        {
           queryable = queryable.Where(x => x.Name.StartsWith(request.UnitName));
        }     
        return  await PagedList<UnitListDto>.ToPagedList(queryable.ProjectTo<UnitListDto>(_iMapper.ConfigurationProvider).AsNoTracking(), request.PageIndex, request.PageSize);
        
    }
}
