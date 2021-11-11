using Application.RepositoryInterfaces;
using Domain.Tickets;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(KastelPlannerDbContext context) : base(context)
        {
        }
    }
}
