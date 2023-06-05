using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Configurations;
public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.City).IsRequired().HasMaxLength(50);
        builder.Property(x => x.StreetName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.HouseNumber).IsRequired();
        builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(8);
        builder.HasOne(x => x.Creator);

        //seeding
        builder.HasData(new
        {
            Id = 1,
            City = "Huizingen",
            ZipCode = "1654",
            HouseNumber = 42,
            StreetName = "Vaucampslaan",
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            CreatorId = 2
        }, new
        {
            Id = 2,
            City = "Gent",
            ZipCode = "9050",
            HouseNumber = 4,
            StreetName = "Gaston Crommenlaan",
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        }, new
        {
            Id = 3,
            City = "Kontich",
            ZipCode = "2550",
            HouseNumber = 26,
            StreetName = "Prins Boudewijnlaan",
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            CreatorId = 2
        });
    }
}
