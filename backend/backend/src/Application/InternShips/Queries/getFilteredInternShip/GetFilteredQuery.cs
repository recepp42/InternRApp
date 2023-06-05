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
using backend.Domain.Entities;
using MediatR;
using backend.Application.InternShips.Queries.GetAllInternShips;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using backend.Application.InternShips.Queries.GetInternShipById;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Enums;

namespace backend.Application.InternShips.Queries.getFilteredInternShip;

public class GetFilteredQuery : IRequest<PagedList<InternShipListDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public List<int> UnitIds { get; set; }
    public List<string> SchoolYear { get; set; }
    public List<int> LanguageIds { get; set; }

}
public class GetFilteredInterShipsQueryHandler : IRequestHandler<GetFilteredQuery, PagedList<InternShipListDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private IMapper _iMapper;

    public GetFilteredInterShipsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _iMapper = mapper;
    }

    public async Task<PagedList<InternShipListDto>> Handle(GetFilteredQuery request, CancellationToken cancellationToken)
    {
    
        return await PagedList<InternShipListDto>.ToPagedList(_dbContext.InternShips
            .Where(internschip => (request.UnitIds == null || request.UnitIds.Count == 0 || request.UnitIds.Contains(internschip.UnitId))
                && (request.SchoolYear == null || request.SchoolYear.Count == 0 || request.SchoolYear.Contains(internschip.SchoolYear))
                && (request.LanguageIds == null || request.LanguageIds.Count == 0 || internschip.Translations.Any(trnsl => request.LanguageIds.Contains(trnsl.LanguageId))))
            .ProjectTo<InternShipListDto>(_iMapper.ConfigurationProvider).AsNoTracking(), request.PageIndex, request.PageSize);
    }

}