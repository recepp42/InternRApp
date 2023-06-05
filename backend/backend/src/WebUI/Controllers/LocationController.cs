using Microsoft.AspNetCore.Mvc;
using MediatR;
using backend.Application.Common.Paging;
using backend.Application.Locations.Queries.GetAllLocations;
using backend.Application.InternShips.Common;
using backend.Application.Locations.Commands.CreateLocation;
using backend.Application.Languages.Commands.CreateLanguage;
using backend.Application.Languages.Commands.UpdateLanguage;
using backend.Application.Locations.Commands.UpdateLocation;
using backend.Application.Languages.Commands.DeleteLanguage;
using backend.Application.Locations.Commands.DeleteLocation;
using CommonReadModels.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers;
[ApiController]
[Route("/api/[Controller]")]

public class LocationController: ControllerBase
{
    private readonly IMediator _mediator;

    public LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByfilterAndPage([FromQuery] LocationFilterAndPaginationDto dto)
    {
        var list = await _mediator.Send(new GetAllQuery() { PageSize=dto.PageSize,City=dto.City,PageIndex=dto.PageIndex });
        return Ok(list);
    }
    [HttpPost()]
    public async Task<IActionResult> PostLocation(CreateLocationDto location)
    {
        await _mediator.Send(new CreateLocationCommand() { city=location.city,housenumber=location.housenumber,streetname=location.streetname,zipcode=location.zipcode });
        return Ok();
    }
    [HttpPatch()]
    public async Task<IActionResult> UpdateLocation(UpdateLocationDto dto)
    {
        await _mediator.Send(new UpdateLocationCommand() { Id=dto.Id,City=dto.City,Housenumber=dto.Housenumber,Streetname=dto.Streetname,Zipcode=dto.Zipcode });
        return Ok();
    }
    [HttpDelete(),Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteLocation([FromBody] List<int> ids)
    {
        if (ids == null || ids.Count <= 0) return BadRequest();
        await _mediator.Send(new DeleteLocationCommand() { Ids =ids });
        return Ok();
    }

}
