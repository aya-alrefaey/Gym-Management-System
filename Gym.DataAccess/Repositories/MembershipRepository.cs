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
    public class MembershipRepository:Repository<Membership>, IMembershipRepository
    {
        private readonly GymDbcontext context;
        public MembershipRepository(GymDbcontext _context) : base(_context)
        {
            context = _context;
        }

        public Task<List<Membership>> GetAllMembershipswithDetailsAsync(CancellationToken cancellationToken = default)
        {
           return context.Memberships
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .Where(m => m.EndDate > DateTime.Now)
                .ToListAsync(cancellationToken);
        }
    }
}
