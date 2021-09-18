using Application.RepositoryInterfaces;
using Domain.Schedules;
using Domain.Schedules.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HostedServices
{
    public class ScheduleHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceScopeFactory _services { get; }

        public ScheduleHostedService(IServiceScopeFactory services)
        {
            _services = services;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {   
            DateTime current = DateTime.Now;
            DateTime nextSunday = new DateTime(current.Year, current.Month, current.Day, 23, 59, 59);
            nextSunday.AddDays(7 - (int)current.DayOfWeek);

            _timer = new Timer(async _ => await ScheduleCleanup(), null, TimeSpan.FromTicks(nextSunday.Ticks - current.Ticks),
                    TimeSpan.FromDays(7));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async Task ScheduleCleanup()
        {
            using var scope = _services.CreateScope();
            var scheduleRepository = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
            var weeklyLogRepository = scope.ServiceProvider.GetRequiredService<IBeneficiaryWeeklyLogRepository>();

            var weeklyLogs = await weeklyLogRepository.GetAllAsync();

            foreach (var weeklyLog in weeklyLogs)
            {
                DateTime currentDate = DateTime.Today;

                int convertedDayOfWeek = ((int)currentDate.DayOfWeek - 1) % 7;
                int daysUntilNextWeeklyLog = (weeklyLog.DayOfWeek.Value - convertedDayOfWeek + 7) % 7;
                DateTime nextWeeklyLog = currentDate.AddDays(daysUntilNextWeeklyLog);
                Schedule previousSchedule = await scheduleRepository.GetFirstByPredicateAsync(s => 
                        s.WeeklyLogId.Equals(weeklyLog.Id));
                var newDuration = Duration.Create(previousSchedule.Duration.Hours, previousSchedule.Duration.Minutes);

                if (newDuration.IsSuccess)
                {
                    if (previousSchedule != null)
                    {
                        Schedule newSchedule = new Schedule(null, weeklyLog.Id, nextWeeklyLog, newDuration.Value);
                        await scheduleRepository.AddAsync(newSchedule);
                    }
                }
            }
        }
    }
}
