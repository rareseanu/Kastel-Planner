using Domain.BeneficiaryWeeklyLogs;
using Domain.BeneficiaryWeeklyLogs.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class BeneficiaryWeeklyLogTypeConfiguration 
            : BasicEntityTypeConfiguration<BeneficiaryWeeklyLog>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<BeneficiaryWeeklyLog> builder)
        {
            builder.ToTable("weekly_log");

            builder.OwnsOne(s => s.Duration, duration =>
            {
                duration.Property(d => d.Minutes)
                    .HasColumnName("duration")
                    .IsRequired();
            });

            builder.Property(w => w.StartTime)
                .HasColumnName("start_time")
                .IsRequired();

            builder.Property(w => w.DayOfWeek)
                .HasConversion(w => w.Name, w => DayOfWeek.FromString(w))
                .HasColumnName("day_of_week")
                .IsRequired();

            builder.Property(w => w.BeneficiaryId)
                .HasColumnName("beneficiary_id")
                .IsRequired();

            builder.HasOne(w => w.Person)
                .WithMany(p => p.BeneficiaryWeeklyLogs)
                .HasForeignKey(w => w.BeneficiaryId);
        }
    }
}
