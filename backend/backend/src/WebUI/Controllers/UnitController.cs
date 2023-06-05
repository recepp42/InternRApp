using backend.Application.Common.Interfaces;
using backend.Application.Common.Paging;
using backend.Application.Units.Commands.CreateUnit;
using backend.Application.Units.Commands.DeleteUnit;
using backend.Application.Units.Commands.UpdateUnit;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Application.Units.Queries.GetAllUnitsWithMinimalInfo;
using backend.Application.Units.Queries.GetUnitById;
using backend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GetAllQueryMinimal = backend.Application.Units.Queries.GetAllUnitsWithMinimalInfo.GetAllQuery;
using GetAllQuery = backend.Application.Units.Queries.GetAllUnits.GetAllQuery;
using CommonReadModels.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers;
[ApiController]
[Route("/api/[Controller]")]
public class UnitController : ControllerBase
{
    private IMediator _mediator;
    public UnitController(IMediator mediator)
    {
        _mediator = mediator;
    }
    //later to be adjusted dependent on what the situation should be see createunitcommand for more details!!
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task Create( CreateUnitCommand command)
    {
         await _mediator.Send(command);
       
    }
    [HttpGet("{id:int}")]
    
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
       return Ok(await _mediator.Send(new GetByIdQuery() { Id=id}));
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllByfilterAndPage([FromQuery] UnitFilterAndPaginationDto dto)
    {
        var list = await _mediator.Send(new GetAllQuery(){PageIndex=dto.PageIndex,UnitName=dto.UnitName,PageSize=dto.PageSize });
        return Ok(list);
    }
    [HttpGet("getAllWithminimaldata")]
    public async Task<IActionResult> GetAllWithMinimumData([FromQuery] UnitFilterAndPaginationDto dto)
    {
        var result = await _mediator.Send(new GetAllQueryMinimal() { PageIndex = dto.PageIndex, UnitName = dto.UnitName, PageSize = dto.PageSize });
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UnitListUpdateDto dto)
    {
        await _mediator.Send(new UpdateUnitCommand() { Id=dto.Id,Name=dto.Name,ManagerEmails=dto.ManagerEmails,PrefaceTranslations=dto.PrefaceTranslations});
        return Ok();
    }

    [HttpDelete,Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        await _mediator.Send(new DeleteUnitCommand() { Ids = ids });
 
        return Ok();
    }
}
