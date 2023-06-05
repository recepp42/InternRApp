using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using FluentValidation;

namespace backend.Application.Locations.Commands.DeleteLocation;
public class DeleteLocationCommandValidator: AbstractValidator<DeleteLocationCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public DeleteLocationCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
       
    }
}
