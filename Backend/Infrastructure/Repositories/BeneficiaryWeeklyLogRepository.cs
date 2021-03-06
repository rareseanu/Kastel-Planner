using Application.RepositoryInterfaces;
using Domain.BeneficiaryWeeklyLogs;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BeneficiaryWeeklyLogRepository : Repository<BeneficiaryWeeklyLog>, IBeneficiaryWeeklyLogRepository
    {
        public BeneficiaryWeeklyLogRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}