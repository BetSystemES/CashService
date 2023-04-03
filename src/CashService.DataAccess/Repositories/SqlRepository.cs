using CashService.BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Polly;

namespace CashService.DataAccess.Repositories
{
    public class SqlRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _entities;

        protected SqlRepository(DbContext dbContext)
        {
            _entities = dbContext.Set<TEntity>();
            _dbContext = dbContext;
        }

        public EntityEntry Attach(TEntity entity)
        {
            return _dbContext.Attach(entity);
        }

        public virtual async Task Add(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            await _entities.AddAsync(entity, token);
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            await _entities.AddRangeAsync(entities, token);
        }

        public virtual Task Remove(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task Update(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            _entities.UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}