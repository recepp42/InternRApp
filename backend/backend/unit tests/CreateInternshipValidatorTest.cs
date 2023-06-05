using backend.Application.Common.Interfaces;
using backend.Application.InternShips.Commands.CreateInternShip;
using backend.Application.InternShips.Common;
using backend.Domain.Entities;
using backend.Domain.Enums;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace unit_tests;
public class CreateInternshipValidatorTest
{
    [Fact]
    public void SchoolYear_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {
            
            CurrentCountOfStudents = 10,
            Locations = new List<LocationDto>
                                                      {
                                                          new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
                                                      },
            MaxCountOfStudents = 20,
            TrainingType = 0,
            UnitId = 1,
            Versions = new List<TranslationCreateInternshipDto>()
                {
                    new(){Comment="test",Description="test",KnowledgeToDevelop="test",LanguageId=1,NeededKnowledge="test",TitleContent="test"}
                }

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.SchoolYear);
    }
    [Fact]
    public void MaxCountOfStudents_Negatif_Or_Zero_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);
        var mockedDbsetOfUnits = new Mock<DbSet<Department>>();
        mockedDbContext.Setup(x => x.Departments).Returns(mockedDbsetOfUnits.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 10,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",
            Locations = new List<LocationDto>
                                                      {
                                                          new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
                                                      },

            TrainingType = 0,
            UnitId = 1,
            Versions = new List<TranslationCreateInternshipDto>()
                {
                    new(){Comment="test",Description="test",KnowledgeToDevelop="test",LanguageId=1,NeededKnowledge="test",TitleContent="test"}
                }

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.MaxCountOfStudents);
    }
    [Fact]
    public void TrainingType_Negatif_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 10,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",
            Locations = new List<LocationDto>
                                                      {
                                                          new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
                                                      },

            MaxCountOfStudents = 10,
            TrainingType = (TrainingType)Enum.Parse(typeof(TrainingType), "-999"),
            UnitId = 1,
            Versions = new List<TranslationCreateInternshipDto>()
                {
                    new(){Comment="test",Description="test",KnowledgeToDevelop="test",LanguageId=1,NeededKnowledge="test",TitleContent="test"}
                }

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.TrainingType);
    }
    [Fact]
    public void CurrentCount_Is_Greater_Than_MaxCount_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 15,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",
            Locations = new List<LocationDto>
                                                      {
                                                          new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
                                                      },

            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Versions = new List<TranslationCreateInternshipDto>()
                {
                    new(){Comment="test",Description="test",KnowledgeToDevelop="test",LanguageId=1,NeededKnowledge="test",TitleContent="test"}
                }

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.CurrentCountOfStudents);
    }
    [Fact]
    public void Locations_Is_Null_Empty_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 8,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Versions = new List<TranslationCreateInternshipDto>()
            {
                new(){Comment="test",Description="test",KnowledgeToDevelop="test",LanguageId=1,NeededKnowledge="test",TitleContent="test"}
            }

        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Locations);
    }
    [Fact]
    public void Versions_Is_Null_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 5,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Locations = new List<LocationDto>
            {
                new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
            },
            



        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Versions);
    }
    
    [Fact]
    public void Version_Comment_Is_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 5,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Locations = new List<LocationDto>
            {
                new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
            },

            Versions = new List<TranslationCreateInternshipDto>()
            {
                new(){
                    Comment=String.Empty,
                    Description="test",
                    KnowledgeToDevelop="test",
                    NeededKnowledge="test",
                    TitleContent="test"
                }
            }


        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Versions);
    }
    [Fact]
    public void Version_Description_Is_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 5,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Locations = new List<LocationDto>
            {
                new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
            },

            Versions = new List<TranslationCreateInternshipDto>()
            {
                new(){
                    Comment="test",
                    Description=String.Empty,
                    KnowledgeToDevelop="test",
                    LanguageId=1,
                    NeededKnowledge="test",
                    TitleContent="test"
                }
            }


        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Versions);
    }
    [Fact]
    public void Version_KnowledgeToDevelop_Is_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 5,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Locations = new List<LocationDto>
            {
                new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
            },

            Versions = new List<TranslationCreateInternshipDto>()
            {
                new(){
                    Comment="test",
                    Description="test",
                    KnowledgeToDevelop=String.Empty,
                    LanguageId=1,
                    NeededKnowledge="test",
                    TitleContent="test"
                }
            }


        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Versions);
    }
    
    [Fact]
    public void Version_NeededKnowledge_Is_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 5,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Locations = new List<LocationDto>
            {
                new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
            },

            Versions = new List<TranslationCreateInternshipDto>()
            {
                new(){
                    Comment="test",
                    Description="test",
                    KnowledgeToDevelop="test",
                    LanguageId=1,
                    NeededKnowledge=String.Empty,
                    TitleContent="test"
                }
            }


        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Versions);
    }
    [Fact]
    public void Version_TitleContent_Is_Empty_Should_Throw_Validation_Error()
    {
        var mockedDbContext = new Mock<IApplicationDbContext>();
        var mockedDbset = new Mock<DbSet<InternShip>>();
        mockedDbContext.Setup(x => x.InternShips).Returns(mockedDbset.Object);

        var validator = new CreateInterShipCommandValidator(mockedDbContext.Object);
        var command = new CreateInternShipCommand()
        {

            CurrentCountOfStudents = 5,
            SchoolYear = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",


            MaxCountOfStudents = 10,
            TrainingType = TrainingType.Master,
            UnitId = 1,
            Locations = new List<LocationDto>
            {
                new(){City="Antwerp", Housenumber=51,Streetname="Ellermanstraat",Zipcode="2260"}
            },

            Versions = new List<TranslationCreateInternshipDto>()
            {
                new(){
                    Comment="test",
                    Description="test",
                    KnowledgeToDevelop="test",
                    LanguageId=1,
                    NeededKnowledge="test",
                    TitleContent=String.Empty
                }
            }


        };
        var result = validator?.TestValidate(command);
        result?.ShouldHaveValidationErrorFor(x => x.Versions);
    }
}
