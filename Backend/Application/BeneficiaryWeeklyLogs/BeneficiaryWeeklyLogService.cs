using Application.BeneficiaryWeeklyLogs.Requests;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.BeneficiaryWeeklyLogs;
using Application.RepositoryInterfaces;

namespace Application.BeneficiaryWeeklyLogs.Responses
{
    public sealed class BeneficiaryWeeklyLogService : IBeneficiaryWeeklyLogService
    {
        private readonly IBeneficiaryWeeklyLog _weeklyLogRepository;
        private TimeSpan startTime;
        private Guid benecifiaryId;

        public BeneficiaryWeeklyLogService(IBeneficiaryWeeklyLog weeklyLogRepository)
        {
            _weeklyLogRepository = weeklyLogRepository;
        }

        public async Task<Result> CreateWeeklyLogAsync(CreateBeneficiaryWeeklyLogRequest request)
        {
            Result<Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek> dayOfWeekOrError = Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek.Create(request?.DayOfWeek);
            startTime = request.StartTime;
            benecifiaryId = request.BeneficiaryId;

            if (dayOfWeekOrError.IsFailure)
            {
                return Result.Failure(dayOfWeekOrError.Error);
            }

            var weeklyLog = new BeneficiaryWeeklyLog(benecifiaryId, startTime, dayOfWeekOrError.Value);

            await _weeklyLogRepository.AddAsync(weeklyLog);

            return Result.Success();
        }

        public async Task<Result> DeleteCompanyAsync(Guid weeklyLogId)
        {
            var weeklyLog = await _weeklyLogRepository.GetByIdAsync(weeklyLogId);

            if (weeklyLog == null)
            {
                return Result.Failure($"Weekly Log with Id {weeklyLogId} was not found");
            }

            await _weeklyLogRepository.Delete(weeklyLog);

            return Result.Success();
        }

        public async Task<IList<BeneficiaryWeeklyLogResponse>> GetAllWeeklyLogsAsync()
        {
            var response = new List<BeneficiaryWeeklyLogResponse>();

            var weeklyLogs = await _weeklyLogRepository.GetAllAsync();

            foreach (var weekly in weeklyLogs)
            {
                var weeklyLogResponse = new BeneficiaryWeeklyLogResponse
                {
                    Id = weekly.Id,
                    StartTime = weekly.StartTime,
                    DayOfWeek = weekly.DayOfWeek,
                    BeneficiaryId = weekly.BeneficiaryId


                };

                response.Add(weeklyLogResponse);
            }

            return response;
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

        public async Task<Result> UpdateCompanyAsync(Guid weeklyLogId, UpdateBeneficiaryWeeklyLog request)
        {
           
            Result<Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek> dayOfWeekOrError = Domain.BeneficiaryWeeklyLogs.ValueObjects.DayOfWeek.Create(request?.DayOfWeek);
            startTime = request.StartTime;
            benecifiaryId = request.BeneficiaryId;

            if (dayOfWeekOrError.IsFailure)
            {
                return Result.Failure(dayOfWeekOrError.Error);
            }

            var weeklyLog = await _weeklyLogRepository.GetByIdAsync(weeklyLogId);


            if (weeklyLog == null)
            {
                return Result.Failure($"Weekly log with id {weeklyLogId} was not found");
            }

            weeklyLog.UpdateBeneficiaryWeeklyLog(request.BeneficiaryId, request.StartTime, dayOfWeekOrError.Value);

            await _weeklyLogRepository.Update(weeklyLog);

            return Result.Success();
        }
    }
}
