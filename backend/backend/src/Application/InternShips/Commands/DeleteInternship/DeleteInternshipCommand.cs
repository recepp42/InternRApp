using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.InternShips.Commands.DeleteInternship;
public class DeleteInternshipCommand:IRequest
{
    public List<int> Ids { get; set; }
}
public class DeleteInternshipCommandHandler : AsyncRequestHandler<DeleteInternshipCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteInternshipCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected async override Task Handle(DeleteInternshipCommand request, CancellationToken cancellationToken)
    {
        var entitiesTobeDeleted = await _dbContext.InternShips.Where(x=>request.Ids.Contains(x.Id)).ToListAsync();
        if (entitiesTobeDeleted == null) return;
        _dbContext.InternShips.RemoveRange(entitiesTobeDeleted);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
