using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class RoleTypeConfiguration : BasicEntityTypeConfiguration<Role>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("role");

            builder.OwnsOne(r => r.RoleName, roleName =>
            {
                roleName.Property(roleName => roleName.Value)
                    .HasColumnName("role")
                    .IsRequired();
            });
        }
    }
}
