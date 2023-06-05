using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using MediatR;

namespace backend.Application.Languages.Commands.CreateLanguage;
public class CreateLanguageCommand:IRequest
{
    public string Name { get; set; }
    public string Code{ get; set; }
}
public class CreateLanguageCommandHandler : AsyncRequestHandler<CreateLanguageCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    public CreateLanguageCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    protected override  async Task Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var entityTobeAdded = new Language() { Name = request.Name,Code=request.Code };
        entityTobeAdded.CreatorId = int.Parse(_currentUser.UserId);
        await _dbContext.Languages.AddAsync(entityTobeAdded);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
