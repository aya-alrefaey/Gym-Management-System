using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public interface IMembershipRepository:IRepository<Membership>
    {
        public Task<List<Membership>> GetAllMembershipswithDetailsAsync(CancellationToken cancellationToken = default);
    }
}
