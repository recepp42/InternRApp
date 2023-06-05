using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Common;
using backend.Domain.Entities;
using backend.Domain.Enums;
using MediatR;

namespace backend.Application.InternShips.Commands.CreateInternShip;
public class CreateInternShipCommand:IRequest
{
    public string SchoolYear { get; set; }
    public int UnitId { get; set; }
    public int MaxCountOfStudents { get; set; }
    public int CurrentCountOfStudents { get; set; }
    public TrainingType TrainingType { get; set; }


    public IList<LocationDto> Locations { get; set; }
    public IList<TranslationCreateInternshipDto> Versions { get; set; }
}
public class CreateInternShipCommandHandler : AsyncRequestHandler<CreateInternShipCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    public CreateInternShipCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    protected async override Task Handle(CreateInternShipCommand request, CancellationToken cancellationToken)
    {
        var convertedLocations = request.Locations.Select(x => new Location() { City = x.City, HouseNumber = x.Housenumber, Id = x.Id, StreetName = x.Streetname, ZipCode = x.Zipcode }).ToList();
        _dbContext.Locations.AttachRange(convertedLocations);
        var result = await _dbContext.InternShips.AddAsync(new() { MaxStudents = request.MaxCountOfStudents, RequiredTrainingType = request.TrainingType, Locations= convertedLocations, SchoolYear = request.SchoolYear, UnitId = request.UnitId,CurrentCountOfStudents=request.CurrentCountOfStudents,CreatorId=int.Parse(_currentUser.UserId) });
        result.Entity.Translations = new List<InternShipContentTranslation>();
        for (int i = 0; i < request.Versions.Count; i++)
        {
            result.Entity.Translations.Add(new()
            {
                InternShipId = result.Entity.Id,
                Comment = request.Versions[i].Comment,
                Description = request.Versions[i].Description,
                KnowledgeToDevelop = request.Versions[i].KnowledgeToDevelop,
                NeededKnowledge = request.Versions[i].NeededKnowledge,
                TitleContent = request.Versions[i].TitleContent,
                LanguageId = request.Versions[i].LanguageId,
                
            });
        }
       
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

