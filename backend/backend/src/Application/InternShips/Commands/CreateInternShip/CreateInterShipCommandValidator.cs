using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Commands.UpdateInternShip;
using backend.Application.InternShips.Common;
using backend.Application.Units.Queries.GetUnitById;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.InternShips.Commands.CreateInternShip;
public class CreateInterShipCommandValidator : AbstractValidator<CreateInternShipCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateInterShipCommandValidator( IApplicationDbContext dbContext)
    {
        
        _dbContext = dbContext;
        CascadeMode = CascadeMode.Stop;
        var validator = new ValidationFunctions(_dbContext);
        RuleFor(x => x).NotEmpty().NotNull();
        RuleFor(x => x.MaxCountOfStudents).Must(x => x > 0);
        RuleFor(x => x.SchoolYear).NotNull().NotEmpty().Must(validator.IsValidSchoolYear);
        RuleFor(x => x.Locations).NotNull();
        RuleFor(x => x.TrainingType).IsInEnum();
        RuleFor(x => x.Versions).NotEmpty().NotNull().Must(ValidationFunctions.IsVersionValid);
        RuleFor(x => x.CurrentCountOfStudents).GreaterThanOrEqualTo(0).LessThanOrEqualTo(x => x.MaxCountOfStudents);
    }
    
}
