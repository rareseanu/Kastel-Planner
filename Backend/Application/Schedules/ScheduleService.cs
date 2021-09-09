using Application.RepositoryInterfaces;
using Application.Schedules.Requests;
using Application.Schedules.Responses;
using Domain;
using Domain.Schedules;
using Domain.Schedules.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Schedules
{
    public sealed class ScheduleService : IScheduleService
    {
        private readonly ISchedule _scheduleRepository;

        public ScheduleService(ISchedule scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Result> CreateScheduleAsync(CreateScheduleRequest request)
        {
            Result<Duration> durationNameOrError = Duration.Create(request.Hours, request.Minutes);
            if (durationNameOrError.IsFailure)
            {
                return Result.Failure(durationNameOrError.Error);
            }

            var schedule = new Schedule(request.VolunteerId, request.WeeklyLogId, request.Date, durationNameOrError.Value);

            await _scheduleRepository.AddAsync(schedule);

            return Result.Success();
        }

        public async Task<Result> DeleteScheduleAsync(Guid scheduleId)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);

            if (schedule == null)
            {
                return Result.Failure($"Schedule with Id {scheduleId} was not found");
            }

            await _scheduleRepository.Delete(schedule);

            return Result.Success();
        }

        public async Task<IList<ScheduleResponse>> GetAllSchedulesAsync()
        {
            var response = new List<ScheduleResponse>();

            var schedules = await _scheduleRepository.GetAllAsync();

            foreach (var schedule in schedules)
            {
                var scheduleResponse = new ScheduleResponse
                {
                    Id = schedule.Id,
                    Hours = schedule.Duration.Hours,
                    Minutes = schedule.Duration.Minutes,
                    VolunteerId = schedule.VolunteerId,
                    WeeklyLogId = schedule.WeeklyLogId
                };

                response.Add(scheduleResponse);
            }

            return response;
        }

        public async Task<Result<ScheduleResponse>> GetScheduleByAsync(Guid id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);

            if (schedule == null)
            {
                return Result.Failure<ScheduleResponse>($"Schedule with Id {id} was not found");
            }

            var response = new ScheduleResponse()
            {
                Id = schedule.Id,
                Hours = schedule.Duration.Hours,
                Minutes = schedule.Duration.Minutes,
                VolunteerId = schedule.VolunteerId,
                WeeklyLogId = schedule.WeeklyLogId
            };

            return Result.Success(response);
        }

        public async Task<Result> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleRequest request)
        {
            Result<Duration> durationNameOrError = Duration.Create(request.Hours, request.Minutes);
            if (durationNameOrError.IsFailure)
            {
                return Result.Failure(durationNameOrError.Error);
            }

            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);

            if (schedule == null)
            {
                return Result.Failure($"Schedule with Id {scheduleId} was not found");
            }

            schedule.UpdateSchedule(request.VolunteerId, request.WeeklyLogId, request.Date, durationNameOrError.Value);

            await _scheduleRepository.Update(schedule);

            return Result.Success();
        }
    }
}
