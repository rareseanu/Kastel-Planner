using Application.Schedules.Requests;
using Application.Schedules.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Schedules
{
    public interface IScheduleService
    {
        Task<IList<ScheduleResponse>> GetAllSchedulesAsync();
        Task<Result<ScheduleResponse>> GetScheduleByAsync(Guid id);
        Task<Result> CreateScheduleAsync(CreateScheduleRequest request);
        Task<Result> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleRequest request);
        Task<Result> DeleteScheduleAsync(Guid scheduleId);
    }
}
