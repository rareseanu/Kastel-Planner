using Application.BeneficiaryWeeklyLogs.Requests;
using Application.BeneficiaryWeeklyLogs.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BeneficiaryWeeklyLogs
{
    public interface IBeneficiaryWeeklyLogService
    {
        Task<IList<BeneficiaryWeeklyLogResponse>> GetAllWeeklyLogsAsync();
        Task<Result<BeneficiaryWeeklyLogResponse>> GetWeeklyLogByAsync(Guid id);
        Task<Result<IList<BeneficiaryWeeklyLogResponse>>> GetAllWeeklyLogsByPersonId(Guid personId);
        Task<Result<BeneficiaryWeeklyLogResponse>> CreateWeeklyLogAsync(CreateBeneficiaryWeeklyLogRequest request);
        Task<Result<BeneficiaryWeeklyLogResponse>> UpdateWeeklyLogAsync(Guid weeklyLogId, UpdateBeneficiaryWeeklyLog request);
        Task<Result> DeleteWeeklyLogAsync(Guid weeklyLogId);
    }
}
