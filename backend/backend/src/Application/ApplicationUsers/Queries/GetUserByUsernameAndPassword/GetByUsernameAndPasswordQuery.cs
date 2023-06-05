using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Application.ApplicationUsers.Queries.GetUserByUsernameAndPassword;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.ApplicationUsers.Queries.GetUserByUserNameAndPassword;
public class GetByUsernameAndPasswordQuery:IRequest<UserDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class GetByUsernameAndPasswordQueryHandler : IRequestHandler<GetByUsernameAndPasswordQuery, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByUsernameAndPasswordQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetByUsernameAndPasswordQuery request, CancellationToken cancellationToken)
    {
        var entity=await _context.ApplicationUsers.Where(x => x.Email == request.Username && x.Password == request.Password).ProjectTo<UserDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        return entity;
    }
}
