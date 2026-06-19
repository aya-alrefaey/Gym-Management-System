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
    public class BookingRepository:Repository<Booking>, IBookingRepository
    {
        private readonly GymDbcontext context ;
        public BookingRepository(GymDbcontext _context): base(_context)
        {
            context = _context;
        }
        
        public async Task<List<Booking>> GetBookingsWithMemberForSessionAsync(int sessionid, CancellationToken cancellationToken = default)
        {
            return await context.Bookings
                .Include(b => b.Member)
                .Where(b => b.SessionId == sessionid)
                .ToListAsync(cancellationToken);
        }
    }
}
