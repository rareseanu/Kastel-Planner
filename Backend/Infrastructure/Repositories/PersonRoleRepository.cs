using Application.RepositoryInterfaces;
using Domain.PersonsRoles;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PersonRoleRepository : Repository<PersonRole>, IPersonRoleRepository
    {
        public PersonRoleRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}