using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public interface ISessionRepository: IRepository<Session>
    {
        public  Task<Session?> SessionsWithCategoryAndTrainerAsync(int id,CancellationToken cancellationToken);
        public Task<Session?> SessionsWithBookingsAsync(int id, CancellationToken cancellationToken);
        public Task<List<Session>> EntireSessionsAsync(CancellationToken cancellationToken);
    }
}
