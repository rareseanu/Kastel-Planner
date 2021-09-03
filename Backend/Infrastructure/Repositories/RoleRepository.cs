using Application.RepositoryInterfaces;
using Domain.Roles;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}