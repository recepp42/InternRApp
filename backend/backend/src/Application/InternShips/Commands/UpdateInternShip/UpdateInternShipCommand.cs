using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Common;
using backend.Domain.Entities;
using backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.InternShips.Commands.UpdateInternShip;
public class UpdateInternShipCommand : IRequest
{
    public int InternShipId { get; set; }
    public string SchoolYear { get; set; }
    public int UnitId { get; set; }
    public byte MaxCountOfStudents { get; set; }
    public TrainingType TrainingType { get; set; }
    public byte CurrentCountOfStudents { get; set; }


    public IList<LocationDto> Locations { get; set; }
    public IList<TranslationUpdateInternshipDto> Versions { get; set; }
}
public class UpdateInternShipCommandHandler : AsyncRequestHandler<UpdateInternShipCommand>
{
    private readonly IApplicationDbContext _dbContext;
    readonly ICurrentUserService _currentUser;
    public UpdateInternShipCommandHandler(IApplicationDbContext dbContext,ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser; 
    }

    protected async override Task Handle(UpdateInternShipCommand request, CancellationToken cancellationToken)
    {
        Role role=(Role)Enum.Parse(typeof(Role),_currentUser.Role);
        var convertedLocations = request.Locations.Select(x => new Location() { City = x.City, HouseNumber = x.Housenumber, Id = x.Id, StreetName = x.Streetname, ZipCode = x.Zipcode }).ToList();
        _dbContext.Locations.UpdateRange(convertedLocations);
        var internShip = await _dbContext.InternShips.Include(x=>x.Locations).Include(x=>x.Translations).SingleOrDefaultAsync(x => x.Id == request.InternShipId);
        if (role != Role.Admin && internShip.CreatorId != int.Parse(_currentUser.UserId))
            throw new UnauthorizedAccessException();
        internShip.MaxStudents = request.MaxCountOfStudents;
        internShip.SchoolYear = request.SchoolYear;
        internShip.RequiredTrainingType = request.TrainingType;
        internShip.Locations=null;
        internShip.UnitId = request.UnitId;
        internShip.CurrentCountOfStudents = request.CurrentCountOfStudents;
        //internShip.Translations = new List<InternShipContentTranslation>();
        var ids=request.Versions.Select(x=>x.TranslationId).Where(x=>x!=null).ToList();
        var existingTranslationsTobeDeleted=internShip.Translations.Select(x => x.Id).Where(x => !ids.Contains(x)).ToList();
        if (existingTranslationsTobeDeleted.Any())
        {
            internShip.Translations.Where(x => existingTranslationsTobeDeleted.Contains(x.Id)).ToList().ForEach(x =>
            {
                internShip.Translations.Remove(x);
            });
        }
    


        foreach (var sendedVersion in request.Versions)
        {
            
            if (sendedVersion.TranslationId!=null)
            {
                if(!internShip.Translations.Any(x => x.Id == sendedVersion.TranslationId))
                {
                    internShip.Translations.Add(new()
                    {
                        Id = (int)sendedVersion.TranslationId,
                        LanguageId = sendedVersion.LanguageId,
                        NeededKnowledge = sendedVersion.NeededKnowledge,
                        KnowledgeToDevelop = sendedVersion.KnowledgeToDevelop,
                        Comment = sendedVersion.Comment,
                        Description = sendedVersion.Description,
                        TitleContent = sendedVersion.TitleContent
                    });
                }
              
            }
            else
            {

           
               internShip.Translations.Add(new()
               {

                   NeededKnowledge = sendedVersion.NeededKnowledge,
                   KnowledgeToDevelop = sendedVersion.KnowledgeToDevelop,
                   Comment = sendedVersion.Comment,
                   Description = sendedVersion.Description,
                   TitleContent = sendedVersion.TitleContent,
                   InternShipId=internShip.Id,
                   LanguageId = sendedVersion.LanguageId,

               });
               
            }

        }
        internShip.Locations = convertedLocations;
        await _dbContext.SaveChangesAsync(cancellationToken);

    }


    


}

