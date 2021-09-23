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
        Task<IList<ScheduleResponse>> GetAllSchedulesAsync(GetSchedulesRequest request);
        Task<Result<ScheduleResponse>> GetScheduleByAsync(Guid id);
        Task<Result<IList<ScheduleResponse>>> GetAllSchedulesByPersonId(Guid personId, GetSchedulesRequest request);
        Task<Result<ScheduleResponse>> CreateScheduleAsync(CreateScheduleRequest request);
        Task<Result<ScheduleResponse>> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleRequest request);
        Task<Result> DeleteScheduleAsync(Guid scheduleId);
    }
}
