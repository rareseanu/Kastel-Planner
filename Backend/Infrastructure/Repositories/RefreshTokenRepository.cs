using Application.RepositoryInterfaces;
using Domain.RefreshTokens;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}
