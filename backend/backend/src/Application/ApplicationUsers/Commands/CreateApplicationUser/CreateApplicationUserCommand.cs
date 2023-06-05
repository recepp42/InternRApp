using AutoMapper;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using backend.Domain.Enums;
using MediatR;
using Unit = MediatR.Unit;

namespace backend.Application.ApplicationUsers.Commands.CreateApplicationUser;
public class CreateApplicationUserCommand : IRequest
{
    public string Email { get; set; }
    public string Password{ get; set; }
}
public class CreateApplicationUserCommandHandler : AsyncRequestHandler<CreateApplicationUserCommand>
{

    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateApplicationUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    protected override async Task Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.ApplicationUsers.AddAsync(new() { Email = request.Email,Password=request.Password,Role=Role.Worker });
        await _dbContext.SaveChangesAsync(cancellationToken);
    }


}

