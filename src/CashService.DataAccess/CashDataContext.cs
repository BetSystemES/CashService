using System.Data;
using CashService.BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel, CancellationToken token)
        {
           return await _profileDbContext.Database.BeginTransactionAsync(isolationLevel, token);
        }
    }
}