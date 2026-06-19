using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
   public interface IRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        public Task<T?> GetByIdAsync(int id,CancellationToken cancellationToken );
        public Task<List<T>> GetAllWithDeletedAsync(CancellationToken cancellationToken);
        public Task<List<T>> GetAllDeletedAsync(CancellationToken cancellationToken);

        Task AddAsync(T entity,CancellationToken cancellationToken);

       
        Task Update(T entity, CancellationToken cancellationToken);

       
        Task SoftDelete(T entity, CancellationToken cancellationToken);

        Task Delete(T entity, CancellationToken cancellationToken);

        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        public Task<T?> FindItemAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
    
