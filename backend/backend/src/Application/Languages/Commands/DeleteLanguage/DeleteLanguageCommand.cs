using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Languages.Commands.DeleteLanguage;
public class DeleteLanguageCommand:IRequest
{
    public List<int> Ids { get; set; }
}
public class DeleteLanguageCommandHandler : AsyncRequestHandler<DeleteLanguageCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteLanguageCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var entitiesTobeDeleted = await _dbContext.Languages.Where(x => request.Ids.Contains(x.Id)).FirstOrDefaultAsync();
        if (entitiesTobeDeleted == null)return;
        _dbContext.Languages.RemoveRange(entitiesTobeDeleted);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
