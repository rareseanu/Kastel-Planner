using Domain.Schedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public sealed class ScheduleTypeConfiguration : BasicEntityTypeConfiguration<Schedule>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("schedule");

            builder.OwnsOne(s => s.Duration, duration =>
            {
                duration.Property(d => d.Minutes)
                    .HasColumnName("duration")
                    .IsRequired();
            });

            builder.Property(s => s.Date)
                .HasColumnName("date")
                .IsRequired();

            builder.Property(s => s.WeeklyLogId)
                .HasColumnName("weekly_log_id")
                .IsRequired();

            builder.Property(s => s.VolunteerId)
                .HasColumnName("volunteer_id");

            builder.HasOne(s => s.Volunteer)
                .WithMany(v => v.Schedules)
                .HasForeignKey(s => s.VolunteerId);

            builder.HasOne(s => s.BeneficiaryWeeklyLog)
                .WithMany(w => w.Schedules)
                .HasForeignKey(s => s.WeeklyLogId);
        }
    }
}
