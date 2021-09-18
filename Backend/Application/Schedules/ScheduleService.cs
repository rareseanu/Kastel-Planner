using Application.RepositoryInterfaces;
using Application.Schedules.Requests;
using Application.Schedules.Responses;
using Domain;
using Domain.Schedules;
using Domain.Persons;
using Domain.Schedules.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Schedules
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IBeneficiaryWeeklyLogRepository _weeklyLogRepository;
        private readonly IPersonRepository _personRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IBeneficiaryWeeklyLogRepository weeklyLogRepository,
                IPersonRepository personRepository)
        {
            _scheduleRepository = scheduleRepository;
            _weeklyLogRepository = weeklyLogRepository;
            _personRepository = personRepository;
        }

        public async Task<Result<ScheduleResponse>> CreateScheduleAsync(CreateScheduleRequest request)
        {
            Result<Duration> durationNameOrError = Duration.Create(request.Hours, request.Minutes);
            if (durationNameOrError.IsFailure)
            {
                return Result.Failure<ScheduleResponse>(durationNameOrError.Error);
            }

            var schedule = new Schedule(request.VolunteerId, request.WeeklyLogId, request.Date, durationNameOrError.Value);

            await _scheduleRepository.AddAsync(schedule);

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
                var weeklyLog = await _weeklyLogRepository.GetByIdAsync(schedule.WeeklyLogId);
                var beneficiary = await _personRepository.GetByIdAsync(weeklyLog.BeneficiaryId);
                Person volunteer = null;
                if (schedule.VolunteerId.HasValue)
                {
                    volunteer = await _personRepository.GetByIdAsync(schedule.VolunteerId.Value);
                }

                var scheduleResponse = new ScheduleResponse()
                {
                    Id = schedule.Id,
                    Hours = schedule.Duration.Hours,
                    Minutes = schedule.Duration.Minutes,
                    VolunteerId = schedule.VolunteerId,
                    VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                    VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName,
                    WeeklyLogId = schedule.WeeklyLogId,
                    BeneficiaryFirstName = beneficiary.Name.FirstName,
                    BeneficiaryLastName = beneficiary.Name.LastName,
                    StartTime = weeklyLog.StartTime,
                    DayOfWeek = weeklyLog.DayOfWeek.Name,
                    Date = schedule.Date
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

        public async Task<Result<ScheduleResponse>> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleRequest request)
        {
            Result<Duration> durationNameOrError = Duration.Create(request.Hours, request.Minutes);
            if (durationNameOrError.IsFailure)
            {
                return Result.Failure<ScheduleResponse>(durationNameOrError.Error);
            }

            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);

            if (schedule == null)
            {
                return Result.Failure<ScheduleResponse>($"Schedule with Id {scheduleId} was not found");
            }

            schedule.UpdateSchedule(request.VolunteerId, request.WeeklyLogId, request.Date, durationNameOrError.Value);

            await _scheduleRepository.Update(schedule);

            Person volunteer = null;
            if (schedule.VolunteerId.HasValue)
            {
                volunteer = await _personRepository.GetByIdAsync(schedule.VolunteerId.Value);
            }

            var response = new ScheduleResponse()
            {
                Id = schedule.Id,
                Hours = schedule.Duration.Hours,
                Minutes = schedule.Duration.Minutes,
                VolunteerId = schedule.VolunteerId,
                WeeklyLogId = schedule.WeeklyLogId,
                VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName
            };

            return Result.Success(response);
        }
    }
}
