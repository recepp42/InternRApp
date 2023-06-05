using System.Collections;
using backend.Application.Common.Interfaces;
using backend.Application.Common.Paging;
using backend.Application.InternShips.Commands.CopyInternshipToNextYear;
using backend.Application.InternShips.Commands.CreateInternShip;
using backend.Application.InternShips.Commands.DeleteInternship;
using backend.Application.InternShips.Commands.UpdateInternShip;
using backend.Application.InternShips.Queries.GetExportInternShipData;
using backend.Application.InternShips.Queries.getFilteredInternShip;
using backend.Application.InternShips.Queries.GetInternShipById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;


namespace WebUI.Controllers;
[ApiController]
[Route("/api/[Controller]")]
public class InternShipController : ControllerBase
{
    private readonly IMediator _mediator;
    public InternShipController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InternShipCreateDto dto)
    {

        await _mediator.Send(new CreateInternShipCommand()
        {
            CurrentCountOfStudents = dto.CurrentCountOfStudents,
            Locations = dto.Locations,
            MaxCountOfStudents = dto.MaxCountOfStudents,
            TrainingType = dto.TrainingType,
            SchoolYear = dto.SchoolYear,
            UnitId = dto.UnitId,
            Versions = dto.Versions
        });
        return Ok();
    }
    [HttpPost, Route("copyToNextYear")]
    public async Task<IActionResult> Create([FromBody] List<int> ids)
    {
        await _mediator.Send(new CopyInternshipToNextYearCommand() { IdsOfExistingInternships = ids });

        return Ok();
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetByIdQuery() { Id = id });
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetFiltered([FromQuery] InternShipFilteredDto dto)
    {
        return Ok(await _mediator.Send(new GetFilteredQuery() { PageIndex = dto.PageIndex, PageSize = dto.PageSize, LanguageIds = dto.LanguageIds, SchoolYear = dto.SchoolYear, UnitIds = dto.UnitIds }));
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] InternshipExportRequestDto request, [FromServices] IApplicationDbContext dbContext)
    {
        //Query data ophalen
        List<UnitExportDto> exportData = await _mediator.Send(new GetExportInterShipQuery()
        {
            LanguageId = request.LanguageId,
            SchoolYear = request.SchoolYear,
            UnitIds = request.UnitIds,
        });

        //Genereren van export doc
        Exporting exporting = new Exporting(dbContext);
        string exportPath = exporting.GenerateWordDocFilePath(exportData, request);
        return File(System.IO.File.OpenRead(exportPath), "application/octet-stream", "internships.docx");
    }

    [HttpPut]
    public async Task<IActionResult> Update(InternShipUpdateDto dto)
    {
        await _mediator.Send(new UpdateInternShipCommand()
        {
            Versions = dto.Versions,
            CurrentCountOfStudents = dto.CurrentCountOfStudents,
            InternShipId = dto.InternShipId,
            Locations = dto.Locations,
            MaxCountOfStudents = dto.MaxCountOfStudents,
            SchoolYear = dto.SchoolYear,
            TrainingType = dto.TrainingType,
            UnitId = dto.UnitId,
        });
        return Ok();
    }
    [HttpDelete("{id:int}"),Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete([FromBody] List<int> ids)
    {
        await _mediator.Send(new DeleteInternshipCommand() { Ids = ids });
        return Ok();
    }

}
