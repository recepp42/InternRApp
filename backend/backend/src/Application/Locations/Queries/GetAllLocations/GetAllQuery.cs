using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.Common.Interfaces;
using backend.Application.Common.Paging;
using backend.Application.InternShips.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Locations.Queries.GetAllLocations;
public class GetAllQuery:IRequest<PagedList<LocationDto>> 
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string City { get; set; }
}

public class getAllQueryHandler : IRequestHandler<GetAllQuery, PagedList<LocationDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private IMapper _iMapper;

    public getAllQueryHandler(IApplicationDbContext dbContext, IMapper iMapper)
    {
        _dbContext = dbContext;
        _iMapper = iMapper;
    }

    public async Task<PagedList<LocationDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var queryable = _dbContext.Locations.AsQueryable();
        if (request.City != null && request.City != "")
        {
                queryable = queryable.Where(x => x.City.StartsWith(request.City));
        }
        return await PagedList<LocationDto>.ToPagedList(queryable.ProjectTo<LocationDto>(_iMapper.ConfigurationProvider).AsNoTracking(), request.PageIndex, request.PageSize);
    }
}
