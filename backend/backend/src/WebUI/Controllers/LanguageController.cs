using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using backend.Application.Languages.Commands.CreateLanguage;
using backend.Application.Languages.Commands.DeleteLanguage;
using backend.Application.Languages.Commands.UpdateLanguage;
using backend.Application.Languages.Queries.GetAllLanguages;
using backend.Application.Languages.Queries.GetLanguageById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonReadModels.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers;
[ApiController]
[Route("/api/[Controller]")]
public class LanguageController : ControllerBase
{
    private readonly IMediator _mediator;

    public LanguageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByfilterAndPage([FromQuery] LanguageFilterAndPaginationDto dto)
    {
        var list = await _mediator.Send(new GetAllQuery() { LanguageCode=dto.LanguageCode,PageIndex=dto.PageIndex,PageSize=dto.PageSize });
        return Ok(list);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var language = await _mediator.Send(new GetByIdQuery() { Dto = new() { Id = id } });
        return Ok(language);
    }
    [HttpPost()]
    public async Task<IActionResult> PostLanguage(CreateLanguageDto dto)
    {
        await _mediator.Send(new CreateLanguageCommand() { Name=dto.Name,Code=dto.Code });
        return Ok();
    }
    [HttpPatch()]
    public async Task<IActionResult> UpdateLanguage(UpdateLanguageDto dto)
    {
        await _mediator.Send(new UpdateLanguageCommand() { Code=dto.Code,Name=dto.Name,Id=dto.Id });
        return Ok();
    }
    [HttpDelete(),Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteLanguage([FromBody]List<int> ids)
    {
        await _mediator.Send(new DeleteLanguageCommand() { Ids=ids });
        return Ok();
    }
    [HttpGet("{language}"),AllowAnonymous]
    public async Task<IActionResult> GetLocalizationFileAsDict([FromRoute] string language)
    {
       
        ResourceManager rm = new ResourceManager($"WebUI.Resources.{language.ToString()}",Assembly.GetExecutingAssembly());
        var myResourceSet = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
        var resourceSet=myResourceSet.GetEnumerator();
        Dictionary<string, string> translations = new();
        while(resourceSet.MoveNext())
        {

            translations.Add(resourceSet.Key.ToString(), resourceSet.Value.ToString());
            
        }
        return Ok(translations);
    }

}
