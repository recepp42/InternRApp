using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace backend.Application.Languages.Commands.CreateLanguage;
public class CreateLanguageValidator:AbstractValidator<CreateLanguageCommand>
{
    public CreateLanguageValidator()
    {
        this.CascadeMode = CascadeMode.Stop;
        RuleFor(x=>x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Code).NotNull().NotEmpty();

    }
}
