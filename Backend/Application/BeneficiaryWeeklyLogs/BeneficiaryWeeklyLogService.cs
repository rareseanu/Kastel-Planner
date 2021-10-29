using Application.BeneficiaryWeeklyLogs.Requests;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.BeneficiaryWeeklyLogs;
using Application.RepositoryInterfaces;
using Application.BeneficiaryWeeklyLogs.Responses;
using Domain.BeneficiaryWeeklyLogs.ValueObjects;
using Application.Schedules.Responses;
using Domain.Persons;

namespace Application.BeneficiaryWeeklyLogs
{
    public class BeneficiaryWeeklyLogService : IBeneficiaryWeeklyLogService
    {
        private readonly IBeneficiaryWeeklyLogRepository _weeklyLogRepository;
        private IScheduleRepository _scheduleRepository;
        private IPersonRepository _personRepository;
        private TimeSpan startTime;
        private Guid benecifiaryId;

        public BeneficiaryWeeklyLogService(IBeneficiaryWeeklyLogRepository weeklyLogRepository, IPersonRepository personRepository,
                IScheduleRepository scheduleRepository)
        {
            _weeklyLogRepository = weeklyLogRepository;
            _personRepository = personRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Result<BeneficiaryWeeklyLogResponse>> CreateWeeklyLogAsync(CreateBeneficiaryWeeklyLogRequest request)
        {
            Result<Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek> dayOfWeekOrError = Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek.Create(request?.DayOfWeek);
            startTime = request.StartTime;
            benecifiaryId = request.BeneficiaryId;
            if (dayOfWeekOrError.IsFailure)
            {
                return Result.Failure<BeneficiaryWeeklyLogResponse>(dayOfWeekOrError.Error);
            }

            Result<Duration> durationNameOrError = Duration.Create(request.Hours, request.Minutes);
            if(durationNameOrError.IsFailure)
            {
                return Result.Failure<BeneficiaryWeeklyLogResponse>(dayOfWeekOrError.Error);
            }

            var weeklyLog = new BeneficiaryWeeklyLog(benecifiaryId, startTime, dayOfWeekOrError.Value, durationNameOrError.Value);

            await _weeklyLogRepository.AddAsync(weeklyLog);

            BeneficiaryWeeklyLogResponse weeklyResponse = new BeneficiaryWeeklyLogResponse()
            {
                Id = weeklyLog.Id,
                StartTime = weeklyLog.StartTime,
                DayOfWeek = weeklyLog.DayOfWeek,
                BeneficiaryId = weeklyLog.BeneficiaryId
            };

            return Result.Success(weeklyResponse);
        }

        public async Task<Result> DeleteWeeklyLogAsync(Guid weeklyLogId)
        {
            var weeklyLog = await _weeklyLogRepository.GetByIdAsync(weeklyLogId);

            if (weeklyLog == null)
            {
                return Result.Failure($"Weekly Log with Id {weeklyLogId} was not found");
            }

            await _weeklyLogRepository.Delete(weeklyLog);

            return Result.Success();
        }

        public async Task<IList<BeneficiaryWeeklyLogResponse>> GetAllWeeklyLogsAsync(GetBeneficiaryWeeklyLogRequest request)
        {
            DateTime endTime = request.EndTime == null ? DateTime.MaxValue : request.EndTime.Value;
            var response = new List<BeneficiaryWeeklyLogResponse>();

            var weeklyLogs = await _weeklyLogRepository.GetAllAsync();

            foreach (var weekly in weeklyLogs)
            {
                var beneficiary = await _personRepository.GetByIdAsync(weekly.BeneficiaryId);
                var weeklyLogResponse = new BeneficiaryWeeklyLogResponse()
                {
                    Id = weekly.Id,
                    StartTime = weekly.StartTime,
                    DayOfWeek = weekly.DayOfWeek,
                    BeneficiaryId = weekly.BeneficiaryId,
                    BeneficiaryFirstName = beneficiary.Name.FirstName,
                    BeneficiaryLastName = beneficiary.Name.LastName,
                    Duration = weekly.Duration.Minutes
                };

                if (request.StartTime != null)
                {
                    var thisWeeksSchedule = await _scheduleRepository.GetFirstByPredicateAsync(s => s.WeeklyLogId.Equals(weeklyLogResponse.Id) &&
                            s.Date >= request.StartTime && s.Date <= endTime);

                    if(thisWeeksSchedule != null)
                    {
                        Person volunteer = null;
                        if (thisWeeksSchedule.VolunteerId.HasValue)
                        {
                            volunteer = await _personRepository.GetByIdAsync(thisWeeksSchedule.VolunteerId.Value);
                        }

                        var scheduleResponse = new ScheduleResponse()
                        {
                            Id = thisWeeksSchedule.Id,
                            VolunteerId = thisWeeksSchedule.VolunteerId,
                            VolunteerFirstName = volunteer == null ? null : volunteer.Name.FirstName,
                            VolunteerLastName = volunteer == null ? null : volunteer.Name.LastName,
                            WeeklyLogId = thisWeeksSchedule.WeeklyLogId,
                            Date = thisWeeksSchedule.Date
                        };

                        weeklyLogResponse.Schedule = scheduleResponse;
                    }
                }

                response.Add(weeklyLogResponse);
            }

            return response;
        }

        public async Task<Result<IList<BeneficiaryWeeklyLogResponse>>> GetAllWeeklyLogsByPersonId(Guid personId)
        {
            var beneficiary = await _personRepository.GetByIdAsync(personId);
            if (beneficiary == null)
            {
                return Result.Failure<IList<BeneficiaryWeeklyLogResponse>>($"Person with Id {personId} was not found");
            }

            var weeklyLogs = await _weeklyLogRepository.GetAllByPredicateAsync(s => s.BeneficiaryId.Equals(personId));
            var response = new List<BeneficiaryWeeklyLogResponse>();

            foreach (var weeklyLog in weeklyLogs)
            {
                var weeklyLogResponse = new BeneficiaryWeeklyLogResponse()
                {
                    Id = weeklyLog.Id,
                    StartTime = weeklyLog.StartTime,
                    DayOfWeek = weeklyLog.DayOfWeek,
                    BeneficiaryId = personId
                };

                response.Add(weeklyLogResponse);
            }

            return Result.Success<IList<BeneficiaryWeeklyLogResponse>>(response);
        }

        public async Task<Result<BeneficiaryWeeklyLogResponse>> GetWeeklyLogByAsync(Guid id)
        {
            var weeklyLog = await _weeklyLogRepository.GetByIdAsync(id);

            if (weeklyLog == null)
            {
                return Result.Failure<BeneficiaryWeeklyLogResponse>($"Weekly Log with Id {id} was not found");
            }

            var response = new BeneficiaryWeeklyLogResponse()
            {
                Id = weeklyLog.Id,
                StartTime = weeklyLog.StartTime,
                DayOfWeek = weeklyLog.DayOfWeek,
                BeneficiaryId = weeklyLog.BeneficiaryId
            };

            return Result.Success(response);
        }

        public async Task<Result<BeneficiaryWeeklyLogResponse>> UpdateWeeklyLogAsync(Guid weeklyLogId, UpdateBeneficiaryWeeklyLog request)
        {
           
            Result<Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek> dayOfWeekOrError = Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek.Create(request?.DayOfWeek);
            startTime = request.StartTime;
            benecifiaryId = request.BeneficiaryId;

            if (dayOfWeekOrError.IsFailure)
            {
                return Result.Failure<BeneficiaryWeeklyLogResponse>(dayOfWeekOrError.Error);
            }

            Result<Duration> durationNameOrError = Duration.Create(request.Hours, request.Minutes);
            if (durationNameOrError.IsFailure)
            {
                return Result.Failure<BeneficiaryWeeklyLogResponse>(durationNameOrError.Error);
            }

            var weeklyLog = await _weeklyLogRepository.GetByIdAsync(weeklyLogId);
            if (weeklyLog == null)
            {
                return Result.Failure<BeneficiaryWeeklyLogResponse>($"Weekly log with id {weeklyLogId} was not found");
            }

            weeklyLog.UpdateBeneficiaryWeeklyLog(request.BeneficiaryId, request.StartTime, dayOfWeekOrError.Value, durationNameOrError.Value);

            await _weeklyLogRepository.Update(weeklyLog);

            var response = new BeneficiaryWeeklyLogResponse()
            {
                Id = weeklyLog.Id,
                StartTime = weeklyLog.StartTime,
                DayOfWeek = weeklyLog.DayOfWeek,
                BeneficiaryId = weeklyLog.BeneficiaryId
            };

            return Result.Success(response);
        }
    }
}
