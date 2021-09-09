using Application.BeneficiaryWeeklyLogs.Requests;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BeneficiaryWeeklyLogs.Responses
{
    public interface IBeneficiaryWeeklyLogService
    {
        Task<IList<BeneficiaryWeeklyLogResponse>> GetAllWeeklyLogsAsync();
        Task<Result<BeneficiaryWeeklyLogResponse>> GetWeeklyLogByAsync(Guid id);
        Task<Result> CreateWeeklyLogAsync(CreateBeneficiaryWeeklyLogRequest request);
        Task<Result> UpdateCompanyAsync(Guid weeklyLogId, UpdateBeneficiaryWeeklyLog request);
        Task<Result> DeleteCompanyAsync(Guid weeklyLogId);
    }
}
