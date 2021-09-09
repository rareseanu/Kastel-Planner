using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class UserTypeConfiguration : BasicEntityTypeConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(uu => uu.Value)
                    .HasColumnName("email")
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(uu => uu.PasswordHash)
                    .HasColumnName("password_hash")
                    .IsRequired();
                password.Property(uu => uu.PasswordSalt)
                    .HasColumnName("password_salt")
                    .IsRequired();
            });

            builder.Property(u => u.PersonId)
                .HasColumnName("person_id")
                .IsRequired();

            builder.HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(u => u.PersonId)
                .IsRequired();
        }
    }
}
