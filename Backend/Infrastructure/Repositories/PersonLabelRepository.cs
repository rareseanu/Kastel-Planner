using Application.RepositoryInterfaces;
using Domain.PersonsLabels;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PersonLabelRepository : Repository<PersonLabel>, IPersonLabel
    {
        public PersonLabelRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}