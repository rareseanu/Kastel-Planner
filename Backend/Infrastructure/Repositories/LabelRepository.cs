using Application.RepositoryInterfaces;
using Domain.Labels;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}