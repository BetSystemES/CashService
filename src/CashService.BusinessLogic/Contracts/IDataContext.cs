using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashService.BusinessLogic.Contracts
{
    public interface IDataContext
    {
        /// <summary>Saves the changes.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   Task
        /// </returns>
        Task SaveChanges(CancellationToken token);

        public IExecutionStrategy CreateExecutionStrategy();
        Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel, CancellationToken token);
    }
}
