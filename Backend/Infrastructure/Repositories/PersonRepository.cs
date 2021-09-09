using Application.RepositoryInterfaces;
using Domain.Persons;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}