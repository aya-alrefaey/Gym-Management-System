using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public interface IBookingRepository:IRepository<Booking>
    {
        public Task<List<Booking>> GetBookingsWithMemberForSessionAsync(int id,CancellationToken cancellationToken = default);
    }
}
