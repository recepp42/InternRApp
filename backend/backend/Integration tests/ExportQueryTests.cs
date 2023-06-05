namespace Integration_tests;

using System.Reflection;
using AutoMapper;
using backend.Application.Common.Interfaces;
using backend.Application.Common.Mappings;
using backend.Application.InternShips.Common;
using backend.Application.InternShips.Queries.GetExportInternShipData;
using backend.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

public class ExportQueryTests : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;
    private IServiceScopeFactory _scopeFactory;

    public ExportQueryTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ExportQueryTest()
    {
        //Arrange
        int langId = 1;
        string schoolyear = "2021-2022";
        List<int> unitIds = new List<int>() { };

        ApplicationDbContext _context; // scope.ServiceProvider.GetService<ApplicationDbContext>();

        IMapper _iMapper;

        //mediator 

        //context 
        
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
        optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod()!.Name); //sqlite 
        _context = new(optionsBuilder.Options);   

        
        

        //mapper
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _iMapper = mockMapper.CreateMapper();


        var exportQuery = new GetExportInterShipQuery() { LanguageId = langId, SchoolYear = schoolyear, UnitIds = unitIds };
        var exportQueryHandler = new GetExportInterShipQueryHandler(_context, _iMapper);

        //var exportDataa = await _mediator.Send(exportQuery);

        //Act
        var exportData = await exportQueryHandler.Handle(exportQuery, new CancellationToken());

        foreach (var item in exportData)
        {

        }


        //Assert
        Assert.Single(exportData);
    }
}