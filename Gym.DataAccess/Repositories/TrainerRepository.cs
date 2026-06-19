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
    public class TrainerRepository:Repository<Trainer>, ITrainerRepository
    {
        private readonly GymDbcontext context;
        public TrainerRepository(GymDbcontext _context) : base(_context)
        {

            context = _context;
        }
        public async Task<Trainer?> TrainerWithSessionAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Trainers
           .Include(t => t.Sessions)
           .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}
