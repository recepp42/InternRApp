using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Locations.Commands.UpdateLocation;
public class UpdateLocationCommandValidator:AbstractValidator<UpdateLocationCommand>  
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateLocationCommandValidator(IApplicationDbContext dbContext)
    {
        this.CascadeMode = CascadeMode.Stop;
        _dbContext = dbContext;


        RuleFor(x => x).NotEmpty().NotNull(); //Nodig?
        RuleFor(x => x.City).NotNull().NotEmpty();
        RuleFor(x => x.Housenumber).NotNull().NotEmpty();
        RuleFor(x => x.Zipcode).NotNull().NotEmpty();
    }

    private async Task<bool> IsNameUnique(string arg1, CancellationToken arg2)
    {
        var result = await _dbContext.Locations.Where(x => x.City == arg1).FirstOrDefaultAsync();
        return result == null;
    }
}
