using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.InternShips.Queries.GetInternShipById;
public class GetByIdQuery:IRequest<InternShipDto>
{
    public int Id { get; set; }
}
public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, InternShipDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _iMapper;

    public GetByIdQueryHandler(IApplicationDbContext dbContext, IMapper iMapper)
    {
        _dbContext = dbContext;
        _iMapper = iMapper;
    }

    public async Task<InternShipDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
   
        var result= await _dbContext.InternShips.Where(x => x.Id == request.Id).Include(x=>x.Translations).ProjectTo<InternShipDto>(_iMapper.ConfigurationProvider).AsNoTracking().SingleOrDefaultAsync();
        return result;
    }
}
