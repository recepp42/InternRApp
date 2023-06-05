using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Common;
using backend.Domain.Entities;
using backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Languages.Commands.UpdateLanguage;
public class UpdateLanguageCommand:IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
public class UpdateLanguageCommandHandler : AsyncRequestHandler<UpdateLanguageCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    public UpdateLanguageCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    protected override async Task Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _dbContext.Languages.Where(x => x.Id == request.Id).SingleOrDefaultAsync();
        Role role = (Role)int.Parse(_currentUser.Role);
        if (role != Role.Admin && language.CreatorId != int.Parse(_currentUser.UserId))
            throw new UnauthorizedAccessException();
        language.Name = request.Name;
       language.Code = request.Code;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}
