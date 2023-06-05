using backend.Domain.Entities;
using backend.Domain.Enums;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Duende.IdentityServer.Models.IdentityResources;

namespace backend.Infrastructure.Persistence.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.HasAlternateKey(x => x.Email);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Role).IsRequired();

        builder.HasData(new
        {
            Id = 1,
            Email = "recep@inetum-realdolmen.world",
            Password = "admin123",
            Role = Role.Admin,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
        },
         new
         {
             Id = 2,
             Email = "anton@inetum-realdolmen.world",
             Password = "admin123",
             Role = Role.Worker,
             LastModifiedDate = DateTime.UtcNow,
             CreatedDate = DateTime.UtcNow,
             IsDeleted = false,
         }
        );

    }
}
