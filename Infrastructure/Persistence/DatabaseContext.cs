using Application.Base;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.Shared;
using Infrastructure.Persistence.EntityConfigs.ConatctManagement.People;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Person> People => Set<Person>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<EducationDegree> EducationDegries => Set<EducationDegree>();
        public DbSet<MarriageStatus> MarriageStatuses => Set<MarriageStatus>();

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new PersonConfig());
            
        }
    }
}
