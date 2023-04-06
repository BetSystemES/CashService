using System.Data;
using CashService.BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CashService.DataAccess
{
    public class CashDataContext : IDataContext
    {
        private readonly CashDbContext _profileDbContext;

        public CashDataContext(CashDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }

        /// <summary>Saves the changes.</summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>
        ///   Save changes result
        /// </returns>
        public Task SaveChanges(CancellationToken token)
        {
            return _profileDbContext.SaveChangesAsync(token);
        }
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _profileDbContext.Database.CreateExecutionStrategy();
        }

        public EntityEntry<T> Entry<T>(T entity) where T : class
        {
           return _profileDbContext.Entry(entity);
        }
        
        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel, CancellationToken token)
        {
           return await _profileDbContext.Database.BeginTransactionAsync(isolationLevel, token);
        }
    }
}