using Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class RefreshTokenTypeConfiguration : BasicEntityTypeConfiguration<RefreshToken>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_token");

            builder.OwnsOne(r => r.Token, token =>
            {
                token.Property(rr => rr.Value)
                    .HasColumnName("token")
                    .IsRequired();
            });

            builder.Property(r => r.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(r => r.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired();

            builder.Property(r => r.RevokedAt)
                .HasColumnName("revoked_at");

            builder.HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId);
        }
    }
}
