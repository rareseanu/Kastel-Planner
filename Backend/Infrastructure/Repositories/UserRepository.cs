using Application.RepositoryInterfaces;
using Domain.Users;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUser
    {
        public UserRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}