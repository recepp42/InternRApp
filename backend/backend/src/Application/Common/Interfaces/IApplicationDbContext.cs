using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<InternShip> InternShips { get; }

    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<Department> Departments { get; }

    DbSet<Location> Locations { get; }
    DbSet<Language> Languages { get; }
    DbSet<InternShipContentTranslation> Translations { get; }
    DbSet<PrefaceTranslation> PrefaceTranslations { get; }




    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
