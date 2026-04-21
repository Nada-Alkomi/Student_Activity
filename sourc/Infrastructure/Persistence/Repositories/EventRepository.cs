using Domain.Entities;
using Persistence.Context;
using Services.Abstraction.Repositories;

namespace Persistence.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}