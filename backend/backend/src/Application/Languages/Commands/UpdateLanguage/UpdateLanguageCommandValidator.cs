using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Languages.Commands.UpdateLanguage;
public class UpdateLanguageCommandValidator:AbstractValidator<UpdateLanguageCommand>    
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateLanguageCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        this.CascadeMode = CascadeMode.Stop;
        RuleFor(x => x).MustAsync(IsUniqueLanguage).NotEmpty().NotNull();
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("there is already an existing language");
    }

    private async Task<bool> IsUniqueLanguage(UpdateLanguageCommand command, CancellationToken arg2)
    {
        var result = await _dbContext.Languages.Where(x => x.Name == command.Name&&x.Code==command.Code).FirstOrDefaultAsync();
        return result == null;
    }

 

}
