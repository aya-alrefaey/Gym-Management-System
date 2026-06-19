using Gym.Data.contexts;
using Gym.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public class SessionRepository: Repository<Session>, ISessionRepository
    {
        private readonly GymDbcontext context;
        public SessionRepository(GymDbcontext _context) : base(_context)
        {

            context = _context;
        }

        public async Task<List<Session>> EntireSessionsAsync(CancellationToken cancellationToken)
        {
            return await context.Sessions
        .Include(s => s.Category)
        .Include(s => s.Trainer)
        .Include(s => s.Bookings)
        .ToListAsync(cancellationToken);
        }

        public async Task<Session?> SessionsWithCategoryAndTrainerAsync( int id,CancellationToken cancellationToken)
        {
            return await context.Sessions
        .Include(s => s.Category)
        .Include(s => s.Trainer)
        .Include(s => s.Bookings)
        .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
        public async Task<Session?> SessionsWithBookingsAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Sessions 
        .Include(s => s.Bookings)
        .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}
