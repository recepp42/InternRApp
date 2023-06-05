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

namespace backend.Application.ApplicationUsers.Queries.GetApplicationUserById;
public class GetByIdQuery : IRequest<ApplicationUserDetailDto>
{
    public int Id { get; set; }
}
public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ApplicationUserDetailDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _iMapper;

    public GetByIdQueryHandler(IApplicationDbContext appDbContext, IMapper imapper)
    {
        _dbContext = appDbContext;
        _iMapper = imapper;
    }
    public async Task<ApplicationUserDetailDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.ApplicationUsers.Where(x => x.Id == request.Id).ProjectTo<ApplicationUserDetailDto>(_iMapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}

