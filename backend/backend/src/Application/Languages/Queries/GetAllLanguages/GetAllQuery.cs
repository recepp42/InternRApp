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
using backend.Application.Units.Queries.GetAllUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Languages.Queries.GetAllLanguages;
public class GetAllQuery:IRequest<PagedList<LanguageListDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string LanguageCode { get; set; }
}
public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PagedList<LanguageListDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private IMapper _iMapper;

    public GetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _iMapper = mapper;
    }

    public async Task<PagedList<LanguageListDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var queryable = _dbContext.Languages.AsQueryable();
        if (request.LanguageCode != null && request.LanguageCode != "")
        {
            queryable = queryable.Where(x => x.Code.StartsWith(request.LanguageCode));
            
        }
        return await PagedList<LanguageListDto>.ToPagedList(queryable.ProjectTo<LanguageListDto>(_iMapper.ConfigurationProvider).AsNoTracking(), request.PageIndex, request.PageSize);

    }
}
