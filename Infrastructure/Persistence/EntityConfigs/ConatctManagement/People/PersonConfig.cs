using Domain.Modules.ContactManagement.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigs.ConatctManagement.People
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.OwnsMany(owner =>
                owner.Addresses,
                ownedBuilder => { ownedBuilder.ToTable("PersonAddresses"); }
                );
            builder.OwnsMany(owner =>
                owner.PhoneNumbers,
                ownedBuilder => { ownedBuilder.ToTable("PersonPhoneNumbers"); }
                );
        }
    }
}
