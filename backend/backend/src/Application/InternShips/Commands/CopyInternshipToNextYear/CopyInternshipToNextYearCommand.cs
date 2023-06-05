using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.InternShips.Commands.CopyInternshipToNextYear;
public class CopyInternshipToNextYearCommand:IRequest
{
    public List<int> IdsOfExistingInternships{ get; set; }
}
public class CopyInternshipToNextYearCommandHandler : AsyncRequestHandler<CopyInternshipToNextYearCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    public CopyInternshipToNextYearCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    protected override async Task Handle(CopyInternshipToNextYearCommand request, CancellationToken cancellationToken)
    {
        var internshipsTobeCopiedToNextYear =await _dbContext.InternShips.Where(x => request.IdsOfExistingInternships.Contains(x.Id)).Include(x=>x.Translations).AsNoTracking().ToListAsync();
        var intershipsForTHeNextYear = new List<InternShip>();
        var locations= await _dbContext.InternShips.Where(x => request.IdsOfExistingInternships.Contains(x.Id)).Include(x=>x.Locations).ToListAsync();
        for (int i = 0; i < internshipsTobeCopiedToNextYear.Count; i++)
        {
           // _dbContext.Locations.AttachRange(locations[i].Locations);

            var itemTobeAddedToNextYear = new InternShip()
            {
                Id = 0,
                UnitId= internshipsTobeCopiedToNextYear[i].UnitId,
                SchoolYear = CalculateNextSchoolYear(internshipsTobeCopiedToNextYear[i].SchoolYear),
                CreatorId = int.Parse(_currentUser.UserId),
                Translations = internshipsTobeCopiedToNextYear[i].Translations,
                Locations = locations[i].Locations
        };
            itemTobeAddedToNextYear.Translations.ToList().ForEach(x=>x.Id = 0);
           // internshipsTobeCopiedToNextYear[i].Locations.ToList().ForEach(x => x.Id = 0) ;
            intershipsForTHeNextYear.Add(itemTobeAddedToNextYear);
            
        }
        _dbContext.InternShips.AddRange(intershipsForTHeNextYear);
     
        await _dbContext.SaveChangesAsync(cancellationToken);

    }
    private string CalculateNextSchoolYear(string currentSchoolYear)
    {
        var splittedCurrentSchoolyear = currentSchoolYear.Split("-");
        return $"{int.Parse(splittedCurrentSchoolyear[0])+1}-{int.Parse(splittedCurrentSchoolyear[1]) + 1}";
        
    }
}

