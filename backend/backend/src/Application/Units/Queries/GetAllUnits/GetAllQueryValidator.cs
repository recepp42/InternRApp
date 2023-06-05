using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace backend.Application.Units.Queries.GetAllUnits;
public class GetAllQueryValidator:AbstractValidator<GetAllQuery>    
{
    public GetAllQueryValidator()
    {
        this.CascadeMode = CascadeMode.Stop;
        RuleFor(x => x).NotNull().NotEmpty();
        RuleFor(x => x.PageSize).NotNull().NotEmpty();
        RuleFor(x => x.PageIndex).NotNull().NotEmpty();
    }
}
