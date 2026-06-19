using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gym.Models;
using System.Threading.Tasks;
using Gym.DataAccess.Repositories;

namespace Gym.BusinessLogic.Services
{
    public class Service<T> (IRepository<T> _repo): IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> repo=_repo;
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await repo.GetAllAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllDeletedAsync(CancellationToken cancellationToken)
        {
           return await repo.GetAllDeletedAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllWithDeletedAsync(CancellationToken cancellationToken)
        {
           return await repo.GetAllWithDeletedAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await repo.GetByIdAsync(id, cancellationToken);
        }
       

       
    }
}
