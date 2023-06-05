using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Common;
using FluentValidation;
using MediatR;

namespace backend.Application.InternShips.Commands.UpdateInternShip;
public class UpdateInternShipCommandValidator : AbstractValidator<UpdateInternShipCommand>
{

    private readonly IApplicationDbContext _dbContext;
    public UpdateInternShipCommandValidator( IApplicationDbContext dbContext)
    {
        CascadeMode = CascadeMode.Stop;
        _dbContext = dbContext;
        var validator = new ValidationFunctions( _dbContext);
        RuleFor(x => x).NotEmpty().NotNull();
        RuleFor(x => x.SchoolYear).NotEmpty().Must(validator.IsValidSchoolYear);
        RuleFor(x => x.MaxCountOfStudents).Must(x => x > 0);
        RuleFor(x => x.CurrentCountOfStudents).LessThanOrEqualTo(x => x.MaxCountOfStudents);
        RuleFor(x => x.Locations).NotEmpty().NotNull();
        RuleFor(x => x.TrainingType).IsInEnum();
        RuleFor(x => x.Versions).NotEmpty().NotNull().Must(ValidationFunctions.IsVersionValid);
      
    }


}

