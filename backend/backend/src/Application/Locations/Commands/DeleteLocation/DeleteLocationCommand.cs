using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Locations.Commands.DeleteLocation;
public class DeleteLocationCommand : IRequest
{
    public List<int> Ids { get; set; }
}
public class DeleteLocationCommandHandler : AsyncRequestHandler<DeleteLocationCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteLocationCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var entityTobeDeleted = await _dbContext.Locations.Where(x => request.Ids.Contains(x.Id)).ToListAsync();
        if (entityTobeDeleted == null) return;
        _dbContext.Locations.RemoveRange(entityTobeDeleted);                
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

