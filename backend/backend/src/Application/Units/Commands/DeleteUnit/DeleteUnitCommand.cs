using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Application.Units.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Units.Commands.DeleteUnit;
public class DeleteUnitCommand:IRequest
{
    public List<int> Ids { get; set; }

}
public class DeleteUnitCommandHandler : AsyncRequestHandler<DeleteUnitCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteUnitCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

  
    protected async override Task Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var entitiesTobeDeleted=await _dbContext.Departments.Where(x => request.Ids.Contains(x.Id)).FirstOrDefaultAsync();
        if (entitiesTobeDeleted == null) return;
        _dbContext.Departments.RemoveRange(entitiesTobeDeleted);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
    }
}
