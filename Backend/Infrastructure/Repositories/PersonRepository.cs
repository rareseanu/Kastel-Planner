using Application.RepositoryInterfaces;
using Domain.Persons;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPerson
    {
        public PersonRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}