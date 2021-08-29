using Application.RepositoryInterfaces;
using Domain.Schedules;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ScheduleRepository : Repository<Schedule>, ISchedule
    {
       public ScheduleRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}