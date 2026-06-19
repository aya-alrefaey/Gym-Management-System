using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public interface ITrainerRepository:IRepository<Trainer>
    {
        public Task<Trainer?> TrainerWithSessionAsync(int id, CancellationToken cancellationToken);
    }
}
