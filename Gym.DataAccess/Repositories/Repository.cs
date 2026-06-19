using Gym.Data.contexts;
using Gym.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public class Repository<T>(GymDbcontext context) : IRepository<T> where T : BaseEntity
    {
        private readonly GymDbcontext _dbcontext=context;
        private readonly DbSet<T> _dbset=context.Set<T>();

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _dbset.ToListAsync(cancellationToken );
            
        }

        public async Task<List<T>> GetAllDeletedAsync(CancellationToken cancellationToken)
        {
            return await _dbset.IgnoreQueryFilters().Where(e => e.IsDeleted).ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllWithDeletedAsync(CancellationToken cancellationToken)
        {
            return await _dbset.IgnoreQueryFilters().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbset.FirstOrDefaultAsync(e => e.Id == id,cancellationToken);
            
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbset.AnyAsync(predicate,cancellationToken);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbset.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<T?> FindItemAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbset.FirstOrDefaultAsync(predicate,cancellationToken);
        }



        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbset.AddAsync(entity, cancellationToken);
        }

        public Task Delete(T entity, CancellationToken cancellationToken)
        {
            _dbset.Remove(entity);
            return Task.CompletedTask;

        }

        public Task SoftDelete(T entity, CancellationToken cancellationToken)
        {
           entity.IsDeleted = true;
            return Task.CompletedTask;
        }

        public Task Update(T entity, CancellationToken cancellationToken)
        {
            _dbset.Update(entity);
            return Task.CompletedTask;
        }
       
       
    }
}
