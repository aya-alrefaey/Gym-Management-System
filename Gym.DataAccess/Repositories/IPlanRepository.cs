using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gym.DataAccess.Repositories
{
    public interface IPlanRepository:IRepository<Plan>
    {
      public Task<Plan?> GetPlanWithMembershipsAsync( int id,CancellationToken cancellationToken);
    }
}
