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

namespace backend.Application.Languages.Queries.GetLanguageById;
public class GetByIdQuery:IRequest<LanguageMinimalDataDto>
{
    public LanguageMinimalDataRequest Dto { get; set; }
}
public class GetAllQueryHandler : IRequestHandler<GetByIdQuery, LanguageMinimalDataDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _iMapper;
    public GetAllQueryHandler(IApplicationDbContext dbContext, IMapper iMapper)
    {
        _dbContext = dbContext;
        _iMapper = iMapper;
    }

    public async Task<LanguageMinimalDataDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Languages.Where(x=>x.Id==request.Dto.Id).ProjectTo<LanguageMinimalDataDto>(_iMapper.ConfigurationProvider).AsNoTracking().SingleOrDefaultAsync();
    }
}
