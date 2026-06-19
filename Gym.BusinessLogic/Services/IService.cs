using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface IService<T> where T :BaseEntity
    {
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<T>> GetAllWithDeletedAsync(CancellationToken cancellationToken);
        public Task<List<T>> GetAllDeletedAsync(CancellationToken cancellationToken);
       
      

        
    }
}
