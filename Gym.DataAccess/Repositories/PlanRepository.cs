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
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly GymDbcontext _context;
        public PlanRepository(GymDbcontext context):base(context)
        {
            _context=context;
        }

        public async Task<Plan?> GetPlanWithMembershipsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Plans.Include(p => p.Memberships) 
                .FirstOrDefaultAsync( p => p.Id == id, cancellationToken);
        }
    }
}
