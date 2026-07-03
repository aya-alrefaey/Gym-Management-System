using Gym.DataAccess.Repositories;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        IMemberRepository Members { get; }
        IPlanRepository Plans { get; }
        ITrainerRepository Trainers { get; }
        IRepository<HealthRecord> HealthRecords { get; }
        IRepository<Person>   Persons { get; }
        IRepository<Category> Categories { get; }
        ISessionRepository Sessions { get; }
        IMembershipRepository Memberships { get; }
        IBookingRepository Bookings { get; }


    }
}
