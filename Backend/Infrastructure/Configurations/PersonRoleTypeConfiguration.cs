using Domain.PersonsRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public sealed class PersonRoleTypeConfiguration : BasicEntityTypeConfiguration<PersonRole>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PersonRole> builder)
        {
            builder.ToTable("person_role");

            builder.Property(pr => pr.PersonId)
                .HasColumnName("person_id")
                .IsRequired();

            builder.Property(pr => pr.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            builder.HasOne(pr => pr.Person)
                .WithMany(p => p.PersonRoles)
                .HasForeignKey(pr => pr.PersonId);

            builder.HasOne(pr => pr.Role)
                .WithMany(r => r.PersonRoles)
                .HasForeignKey(pr => pr.RoleId);
        }
    }
}
