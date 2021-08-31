using Domain.PersonsLabels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class PersonLabelTypeConfiguration : BasicEntityTypeConfiguration<PersonLabel>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PersonLabel> builder)
        {
            builder.ToTable("person_label");

            builder.Property(p => p.LabelId)
                .HasColumnName("label_id")
                .IsRequired();

            builder.Property(u => u.PersonId)
                .HasColumnName("person_id")
                .IsRequired();

            builder.HasOne(p => p.Label)
                .WithMany(l => l.PersonLabels)
                .HasForeignKey(p => p.LabelId);

            builder.HasOne(p => p.Person)
                .WithMany(pp => pp.PersonLabels)
                .HasForeignKey(p => p.PersonId);
        }
    }
}
