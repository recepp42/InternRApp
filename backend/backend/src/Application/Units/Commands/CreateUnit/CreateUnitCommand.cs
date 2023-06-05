using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using backend.Application.Common.Interfaces;
using backend.Domain.Entities;
using MediatR;

namespace backend.Application.Units.Commands.CreateUnit;
public class CreateUnitCommand:IRequest
{
    public string Name { get; set; }
    public List<string> SuperVisorEmails { get; set; }
    public List<PrefaceTranslationCreateDto> PrefaceTranslations { get; set; }

}
public class CreateUnitCommandHandler : AsyncRequestHandler<CreateUnitCommand>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateUnitCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _dbContext=applicationDbContext;
    }
    protected async override Task Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var entityTobeAdded = new Department() { Name = request.Name, ManagerEmails = request.SuperVisorEmails };
        var dbAddedEntity=await _dbContext.Departments.AddAsync(entityTobeAdded) ;
        dbAddedEntity.Entity.PrefaceTranslations = new List<PrefaceTranslation>();
        foreach (var prefaceTranslation in request.PrefaceTranslations)
        {
            dbAddedEntity.Entity.PrefaceTranslations.Add(new()
            {
                Content = prefaceTranslation.Content,
                LanguageId = prefaceTranslation.LanguageId,
                
            });
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
