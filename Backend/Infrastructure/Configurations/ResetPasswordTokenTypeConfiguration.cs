using Domain.ResetPasswordTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class ResetPasswordTokenTypeConfiguration : BasicEntityTypeConfiguration<ResetPasswordToken>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ResetPasswordToken> builder)
        {
            builder.ToTable("reset_password_token");

            builder.OwnsOne(r => r.Token, token =>
            {
                token.Property(rr => rr.Value)
                    .HasColumnName("token")
                    .IsRequired();
            });

            builder.Property(r => r.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.ResetPasswordTokens)
                .HasForeignKey(r => r.UserId);
        }
    }
}
