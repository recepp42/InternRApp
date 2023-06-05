using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Application.Units.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Units.Commands.CreateUnit;
public class CreateUnitValidator:AbstractValidator<CreateUnitCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateUnitValidator(IApplicationDbContext dbContext)
    {
        this.CascadeMode = CascadeMode.Stop;
        _dbContext = dbContext;
        RuleFor(x => x).NotEmpty().NotNull();
        RuleFor(x => x.SuperVisorEmails).NotEmpty().NotNull();
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
        RuleFor(x => x.PrefaceTranslations).NotNull().NotEmpty().ForEach(x =>
        {
            x.Must(checkTranslationIsEmpty);
        });
    }
    private  bool checkTranslationIsEmpty(PrefaceTranslationCreateDto prefaceTranslation) => !(String.IsNullOrEmpty(prefaceTranslation.Content));

}
