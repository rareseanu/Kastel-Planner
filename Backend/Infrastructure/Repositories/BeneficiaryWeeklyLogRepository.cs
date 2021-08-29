using Application.RepositoryInterfaces;
using Domain.BeneficiaryWeeklyLogs;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BeneficiaryWeeklyLogRepository : Repository<BeneficiaryWeeklyLog>, IBeneficiaryWeeklyLog
    {
        public BeneficiaryWeeklyLogRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}