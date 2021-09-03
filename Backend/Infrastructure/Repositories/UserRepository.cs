using Application.RepositoryInterfaces;
using Domain.Users;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}