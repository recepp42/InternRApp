using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using FluentValidation;

namespace backend.Application.Languages.Commands.DeleteLanguage;
public class DeleteLanguageCommandValidator:AbstractValidator<DeleteLanguageCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public DeleteLanguageCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
