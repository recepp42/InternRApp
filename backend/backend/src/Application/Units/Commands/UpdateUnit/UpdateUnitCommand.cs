using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit= backend.Domain.Entities.Department;
using MediatR;
using backend.Application.Common.Interfaces;
using backend.Application.Units.Queries.GetUnitById;
using Microsoft.EntityFrameworkCore;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Entities;

namespace backend.Application.Units.Commands.UpdateUnit;
public class UpdateUnitCommand:IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> ManagerEmails { get; set; }
    public IList<PrefaceTranslationUpdateDto> PrefaceTranslations { get; set; }
}
public class UpdateUnitCommandHandler : AsyncRequestHandler<UpdateUnitCommand>
{

    private readonly IApplicationDbContext _dbContext;
    public UpdateUnitCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected async override Task Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        //add control to the validator to check there is an existing item with this id!!
        var unit = await _dbContext.Departments.SingleOrDefaultAsync(x=>x.Id==request.Id);
        unit.Name= request.Name; 
        unit.ManagerEmails=request.ManagerEmails;
        unit.PrefaceTranslations = new List<PrefaceTranslation>();
        foreach (var prefaceTranslation in request.PrefaceTranslations)
        {
            if (prefaceTranslation.TranslationId != null)
            {
                unit.PrefaceTranslations.Add(new()
                {
                    Content = prefaceTranslation.Content,
                    Id=prefaceTranslation.TranslationId??0,
                    LanguageId=prefaceTranslation.LanguageId
                });
            }
            else
            {
                unit.PrefaceTranslations.Add(new()
                {
                    Content = prefaceTranslation.Content,
                    LanguageId = prefaceTranslation.LanguageId
                });
            }
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
