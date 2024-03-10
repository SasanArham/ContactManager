using Domain.Modules.ContactManagement.People;
using Domain.Modules.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Base
{
    public interface IDatabaseContext
    {
        DbSet<Person> People { get; }
        DbSet<Province> Provinces { get; }
        DbSet<City> Cities { get; }
        DbSet<EducationDegree> EducationDegries { get; }
        DbSet<MarriageStatus> MarriageStatuses { get; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
