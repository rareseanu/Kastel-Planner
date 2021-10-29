using Application.RepositoryInterfaces;
using Application.Schedules.Requests;
using Application.Schedules.Responses;
using Domain;
using Domain.Schedules;
using Domain.Persons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BeneficiaryWeeklyLogs.Responses;

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
            var schedule = new Schedule(request.VolunteerId, request.WeeklyLogId, request.Date);

            await _scheduleRepository.AddAsync(schedule);

            var response = new ScheduleResponse()
            {
                Id = schedule.Id,
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

        public async Task<Result<IList<ScheduleResponse>>> GetAllSchedulesByPersonId(Guid personId, GetSchedulesRequest request)
        {
            var volunteer = await _personRepository.GetByIdAsync(personId);
            if (volunteer == null)
            {
                return Result.Failure<IList<ScheduleResponse>>($"Person with Id {personId} was not found");
            }

            DateTime endTime = request.EndTime == null ? DateTime.MaxValue : request.EndTime.Value;

            var schedules = request.StartTime == null ? await _scheduleRepository.GetAllByPredicateAsync(s => s.VolunteerId.Equals(personId))
                : await _scheduleRepository.GetAllByPredicateAsync(s => s.Date.Date >= request.StartTime.Value.Date && s.Date.Date <= endTime.Date
                    && s.VolunteerId.Equals(personId));

            var response = new List<ScheduleResponse>();

            foreach (var schedule in schedules)
            {
                var weeklyLog = await _weeklyLogRepository.GetByIdAsync(schedule.WeeklyLogId);
                var beneficiary = await _personRepository.GetByIdAsync(weeklyLog.BeneficiaryId);

                var weeklyLogResponse = new BeneficiaryWeeklyLogResponse()
                {
                    Id = weeklyLog.Id,
                    StartTime = weeklyLog.StartTime,
                    DayOfWeek = weeklyLog.DayOfWeek,
                    BeneficiaryId = weeklyLog.BeneficiaryId,
                    BeneficiaryFirstName = beneficiary.Name.FirstName,
                    BeneficiaryLastName = beneficiary.Name.LastName,
                    Duration = weeklyLog.Duration.Minutes
                };

                var scheduleResponse = new ScheduleResponse()
                {
                    Id = schedule.Id,
                    VolunteerId = schedule.VolunteerId,
                    VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                    VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName,
                    WeeklyLogId = schedule.WeeklyLogId,
                    Date = schedule.Date,
                    WeeklyLog = weeklyLogResponse
                };

                response.Add(scheduleResponse);
            }

            return Result.Success<IList<ScheduleResponse>>(response);
        }

        public async Task<IList<ScheduleResponse>> GetAllSchedulesAsync(GetSchedulesRequest request)
        {
            DateTime endTime = request.EndTime == null ? DateTime.MaxValue : request.EndTime.Value;

            var response = new List<ScheduleResponse>();
            var schedules = request.StartTime == null ? await _scheduleRepository.GetAllAsync()
                    : await _scheduleRepository.GetAllByPredicateAsync(s => s.Date.Date >= request.StartTime.Value.Date && s.Date.Date <= endTime.Date);

            foreach (var schedule in schedules)
            {
                var weeklyLog = await _weeklyLogRepository.GetByIdAsync(schedule.WeeklyLogId);
                Person volunteer = null;
                var beneficiary = await _personRepository.GetByIdAsync(weeklyLog.BeneficiaryId);

                if (schedule.VolunteerId.HasValue)
                {
                    volunteer = await _personRepository.GetByIdAsync(schedule.VolunteerId.Value);
                }

                var weeklyLogResponse = new BeneficiaryWeeklyLogResponse()
                {
                    Id = weeklyLog.Id,
                    StartTime = weeklyLog.StartTime,
                    DayOfWeek = weeklyLog.DayOfWeek,
                    BeneficiaryId = weeklyLog.BeneficiaryId,
                    BeneficiaryFirstName = beneficiary.Name.FirstName,
                    BeneficiaryLastName = beneficiary.Name.LastName,
                    Duration = weeklyLog.Duration.Minutes
                };

                var scheduleResponse = new ScheduleResponse()
                {
                    Id = schedule.Id,
                    VolunteerId = schedule.VolunteerId,
                    VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                    VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName,
                    WeeklyLogId = schedule.WeeklyLogId,
                    Date = schedule.Date,
                    WeeklyLog = weeklyLogResponse
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

            var weeklyLog = await _weeklyLogRepository.GetByIdAsync(schedule.WeeklyLogId);
            Person volunteer = null;
            var beneficiary = await _personRepository.GetByIdAsync(weeklyLog.BeneficiaryId);

            if (schedule.VolunteerId.HasValue)
            {
                volunteer = await _personRepository.GetByIdAsync(schedule.VolunteerId.Value);
            }

            var weeklyLogResponse = new BeneficiaryWeeklyLogResponse()
            {
                Id = weeklyLog.Id,
                StartTime = weeklyLog.StartTime,
                DayOfWeek = weeklyLog.DayOfWeek,
                BeneficiaryId = weeklyLog.BeneficiaryId,
                BeneficiaryFirstName = beneficiary.Name.FirstName,
                BeneficiaryLastName = beneficiary.Name.LastName,
                Duration = weeklyLog.Duration.Minutes
            };

            var scheduleResponse = new ScheduleResponse()
            {
                Id = schedule.Id,
                VolunteerId = schedule.VolunteerId,
                VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName,
                WeeklyLogId = schedule.WeeklyLogId,
                Date = schedule.Date,
                WeeklyLog = weeklyLogResponse
            };

            return Result.Success(scheduleResponse);
        }

        public async Task<Result<ScheduleResponse>> UpdateScheduleAsync(Guid scheduleId, UpdateScheduleRequest request)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);

            if (schedule == null)
            {
                return Result.Failure<ScheduleResponse>($"Schedule with Id {scheduleId} was not found");
            }

            schedule.UpdateSchedule(request.VolunteerId, request.WeeklyLogId, request.Date);

            await _scheduleRepository.Update(schedule);

            Person volunteer = null;
            if (schedule.VolunteerId.HasValue)
            {
                volunteer = await _personRepository.GetByIdAsync(schedule.VolunteerId.Value);
            }

            var response = new ScheduleResponse()
            {
                Id = schedule.Id,
                VolunteerId = schedule.VolunteerId,
                WeeklyLogId = schedule.WeeklyLogId,
                VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName
            };

            return Result.Success(response);
        }
    }
}
