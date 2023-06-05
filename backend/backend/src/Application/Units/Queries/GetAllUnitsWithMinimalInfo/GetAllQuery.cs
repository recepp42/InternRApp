using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.Common.Interfaces;
using backend.Application.Common.Paging;
using backend.Application.InternShips.Queries.GetAllInternShips;
using backend.Application.Units.Queries.GetAllUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Units.Queries.GetAllUnitsWithMinimalInfo;
public class GetAllQuery:IRequest<PagedList<UnitListDtoWithMinimalData>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string UnitName { get; set; }
}
public class GetAllQueryHandler : IRequestHandler<GetAllQuery,PagedList<UnitListDtoWithMinimalData>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _iMapper;
    public GetAllQueryHandler(IApplicationDbContext dbContext, IMapper iMapper)
    {
        _dbContext = dbContext;
        _iMapper = iMapper;
    }

    public async Task<PagedList<UnitListDtoWithMinimalData>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {

        var queryable = _dbContext.Departments.AsQueryable();
        if (request.UnitName != null && request.UnitName != "")
        {
            queryable = queryable.Where(x => x.Name.StartsWith(request.UnitName));
        }
        return await PagedList<UnitListDtoWithMinimalData>.ToPagedList(queryable.ProjectTo<UnitListDtoWithMinimalData>(_iMapper.ConfigurationProvider).AsNoTracking(), request.PageIndex, request.PageSize);

    }
}
