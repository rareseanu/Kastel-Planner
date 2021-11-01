using Application.RepositoryInterfaces;
using Domain.TicketMessages;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class TicketMessageRepository : Repository<TicketMessage>, ITicketMessageRepository
    {
        public TicketMessageRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}
