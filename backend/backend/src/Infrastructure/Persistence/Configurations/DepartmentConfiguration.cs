using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace backend.Infrastructure.Persistence.Configurations;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
                                     .HasMaxLength(100)
                                     .IsRequired();
        builder.HasIndex(x => x.Name);

        builder.Property(x => x.ManagerEmails).UsePropertyAccessMode(PropertyAccessMode.PreferProperty).HasConversion(v => string.Join(',', v), x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        builder.HasMany(x => x.PrefaceTranslations).WithOne(x => x.Unit).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Internships).WithOne(x => x.Unit).OnDelete(DeleteBehavior.Cascade);

        //seeding
        builder.HasData(new
        {
            Id = 1,
            Name = "Microsoft Competence Center",
            ManagerEmails = new List<string> { "anton.louf@student.ehb.be" },
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        }, new
        {
            Id = 2,
            Name = "Java Competence Center",
            ManagerEmails = new List<string> { "anton.louf@student.ehb.be" },
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        }, new
        {
            Id = 3,
            Name = "Low Code",
            ManagerEmails = new List<string> { "anton.louf@student.ehb.be" },
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        });
        
    }
}
