using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Common;
using FluentValidation;

namespace backend.Application.InternShips.Commands.DeleteInternship;
public class DeleteInternshipCommandValidator:AbstractValidator<DeleteInternshipCommand>
{
    public DeleteInternshipCommandValidator(IApplicationDbContext dbContext)
    {
        
    }
}
