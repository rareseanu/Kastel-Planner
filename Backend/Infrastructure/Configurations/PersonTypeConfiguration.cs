using Domain.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class PersonTypeConfiguration : BasicEntityTypeConfiguration<Person>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person");

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(pp => pp.FirstName)
                    .HasColumnName("first_name");
                name.Property(pp => pp.LastName)
                    .HasColumnName("last_name");
            });

            builder.OwnsOne(pp => pp.PhoneNumber, phoneNumber =>
            {
                phoneNumber.Property(pp => pp.Number)
                    .HasColumnName("phone_number");
            });

            builder.Property(p => p.IsActive)
                .HasColumnName("is_active");
        }
    }
}
