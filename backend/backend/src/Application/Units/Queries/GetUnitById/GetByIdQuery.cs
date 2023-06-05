using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Units.Queries.GetUnitById;
public class GetByIdQuery:IRequest<UnitDetailsDto>
{
    public int Id { get; set; }
}
public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, UnitDetailsDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _iMapper;
    public GetByIdQueryHandler(IApplicationDbContext applicationDbContext,IMapper mapper)
    {
        _dbContext= applicationDbContext;
        _iMapper= mapper;   
    }
    public async Task<UnitDetailsDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result=await _dbContext.Departments.Where(x => x.Id == request.Id).ProjectTo<UnitDetailsDto>(_iMapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
        return result;
    }
}

