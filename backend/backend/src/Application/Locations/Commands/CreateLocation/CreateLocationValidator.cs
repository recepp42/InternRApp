using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace backend.Application.Locations.Commands.CreateLocation;
public class CreateLocationValidator:AbstractValidator<CreateLocationCommand>
{
    public CreateLocationValidator()
    {
        this.CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.city).NotNull().NotEmpty();
        RuleFor(x => x.streetname).NotNull().NotEmpty();
        RuleFor(x => x.housenumber).NotNull().NotEmpty();
        //.Must(x => x.GetType= typeof(int)); 
        RuleFor(x => x.zipcode).NotNull().NotEmpty();
    }
}
