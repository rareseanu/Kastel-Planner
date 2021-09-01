using Domain.Labels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class LabelTypeConfiguration : BasicEntityTypeConfiguration<Label>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Label> builder)
        {
            builder.ToTable("label");

            builder.OwnsOne(l => l.LabelName, label =>
            {
                label.Property(ll => ll.Value)
                    .HasColumnName("name");
            });
        }
    }
}
