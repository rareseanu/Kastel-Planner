using Application.RepositoryInterfaces;
using Domain.ResetPasswordTokens;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ResetPasswordTokenRepository : Repository<ResetPasswordToken>, IResetPasswordTokenRepository
    {
        public ResetPasswordTokenRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}
