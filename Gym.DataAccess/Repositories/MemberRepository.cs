using Gym.Data.contexts;
using Gym.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private readonly GymDbcontext context;
        public MemberRepository(GymDbcontext _context): base(_context) {
        
            context = _context;
        }
        public async Task<Member?> MemberWithMembershipAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Members
         .Include(m => m.Memberships)
          .ThenInclude(ms => ms.Plan)
         .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
        public async Task<Member?> MemberWithBookingAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Members
         .Include(m => m.Bookings)
         .ThenInclude(b=> b.Session)
        .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<Member?> MemberWithMembershipOnlyAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Members
         .Include(m => m.Memberships)
        .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
        public async Task<bool> HasValidMembershipAsync(int memberId, CancellationToken cancellationToken = default)
        {
            return await context.Memberships
                .AnyAsync(m =>
                    m.MemberId == memberId &&
                    m.StartDate <= DateTime.Now &&
                    m.EndDate >= DateTime.Now,
                    cancellationToken);
        }
    }
}
