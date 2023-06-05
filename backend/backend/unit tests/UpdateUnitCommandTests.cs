using backend.Application.Common.Interfaces;
using backend.Application.Units.Commands.CreateUnit;
using backend.Application.Units.Commands.UpdateUnit;
using backend.Application.Units.Queries.GetAllUnits;
using backend.Domain.Entities;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace unit_tests;
public class UpdateUnitCommandTests
{
    [Fact]
    public void SuperVisorEmails_Empty_Should_Throw_Validation_Error()
    {
        var mockDbContext = new Mock<IApplicationDbContext>();
        var unit = new UnitListDto { Id = 1, Name = "Java" };
        var mockDbSet = new Mock<DbSet<Department>>();
        var department = new Department { Id = 1, Name = "Java", ManagerEmails = new List<string> { "jane.doe@example.com" } };

        var command = new UpdateUnitCommand {Id=1
            ,Name=department.Name,PrefaceTranslations=new List<PrefaceTranslationUpdateDto>() 
        {
        new(){Content="zefzef",LanguageId=1,TranslationId=1}
        } };
        mockDbContext.Setup(x => x.Departments).Returns(mockDbSet.Object);
      //  mockDbSet.Setup(x => x.FirstOrDefaultAsync(x => x.Id ==command.Unit.Id)).ReturnsAsync(department);
        var validator = new UpdateUnitCommandValidator(mockDbContext.Object);
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.ManagerEmails);
    }
 
    [Fact]
    public void DepartmentName_Empty_Should_Throw_Validation_Error()
    {
        var mockDbContext = new Mock<IApplicationDbContext>();
        var unit = new UnitListDto { Id=1, ManagerEmails = new List<string> { "jane.doe@example.com" } };
        var mockDbSet = new Mock<DbSet<Department>>();

        var command = new UpdateUnitCommand
        {
            Id = 1
           ,
           
            PrefaceTranslations = new List<PrefaceTranslationUpdateDto>()
        {
        new(){Content="zefzef",LanguageId=1,TranslationId=1}
        }
        };
        mockDbContext.Setup(x => x.Departments).Returns(mockDbSet.Object);
        //  mockDbSet.Setup(x => x.FirstOrDefaultAsync(x => x.Id ==command.Unit.Id)).ReturnsAsync(department);
        var validator = new UpdateUnitCommandValidator(mockDbContext.Object);
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Name);
    }
    [Fact]
    public void PrefaceTranslation_Null_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();

        var validator = new UpdateUnitCommandValidator(mockedDbContext.Object);
        var command = new UpdateUnitCommand()
        {
            Name = "Java",
            ManagerEmails = new() { "recep@inetum-realdolmen.world" },
            Id=1,
            

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.PrefaceTranslations);
    }
    [Fact]
    public void PrefaceTranslation_Content_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();

        var validator = new UpdateUnitCommandValidator(mockedDbContext.Object);
        var command = new UpdateUnitCommand()
        {
            Name = "Java",
            ManagerEmails = new() { "recep@inetum-realdolmen.world" },
            Id=1,
            PrefaceTranslations = new List<PrefaceTranslationUpdateDto>()

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.PrefaceTranslations);
    }
}
