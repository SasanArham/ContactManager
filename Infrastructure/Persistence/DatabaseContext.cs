using Application.Base;
using Domain.Base;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.Shared;
using Infrastructure.Persistence.EntityConfigs.ConatctManagement.People;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly IMediator _mediator;

        public DbSet<Person> People => Set<Person>();
        public DbSet<Province> Provinces => Set<Province>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<EducationDegree> EducationDegries => Set<EducationDegree>();
        public DbSet<MarriageStatus> MarriageStatuses => Set<MarriageStatus>();

        public DatabaseContext(DbContextOptions options
            , IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            // required if you want to implement OutBox pattern with MassTransit
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

            modelBuilder.ApplyConfiguration(new PersonConfig());

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var res = await base.SaveChangesAsync(cancellationToken);

            var entities = ChangeTracker
                    .Entries<BaseEntity>()
                    .Where(e => e.Entity.EventualConsistencyDomainEvents.Any())
                    .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.EventualConsistencyDomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearEventualConsistencyDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent, cancellationToken);

            return res;
        }
    }
}
