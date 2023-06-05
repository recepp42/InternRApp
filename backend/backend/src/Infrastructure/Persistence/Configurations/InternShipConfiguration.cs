using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.Enums;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace backend.Infrastructure.Persistence.Configurations;
public class InternShipConfiguration : IEntityTypeConfiguration<InternShip>
{
    public void Configure(EntityTypeBuilder<InternShip> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RequiredTrainingType).IsRequired();
        builder.HasIndex(x => new { x.SchoolYear });
        builder.Property(x => x.SchoolYear).IsRequired();
        builder.Property(x => x.CurrentCountOfStudents).HasConversion(v => (byte)v,
            v => v)
            .IsRequired();
        builder.HasMany(x => x.Locations).WithMany(x => x.InternShips).UsingEntity<InternShipLocation>();
        builder.Property(x => x.MaxStudents).HasConversion(v => (byte)v,
            v => v).IsRequired();
        builder.HasMany(x => x.Translations).WithOne(x=>x.InternShip).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Creator);

        //seeding
        builder.HasData(new
        {
            Id = 1,
            CurrentCountOfStudents = 0,
            MaxStudents = 4,
            RequiredTrainingType = TrainingType.Bachelor,
            SchoolYear = "2023-2024",
            UnitId = 1,
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        }, new
        {
            Id = 2,
            CurrentCountOfStudents = 0,
            MaxStudents = 4,
            RequiredTrainingType = TrainingType.Master,
            SchoolYear = "2023-2024",
            UnitId = 1,
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            CreatorId=2
        }, new
        {
            Id = 3,
            CurrentCountOfStudents = 0,
            MaxStudents = 4,
            RequiredTrainingType = TrainingType.Bachelor,
            SchoolYear = "2023-2024",
            UnitId = 2,
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        }, new
        {
            Id = 4,
            CurrentCountOfStudents = 0,
            MaxStudents = 4,
            RequiredTrainingType = TrainingType.Bachelor,
            SchoolYear = "2023-2024",
            UnitId = 3,
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            CreatorId = 2
        });

    }
}
