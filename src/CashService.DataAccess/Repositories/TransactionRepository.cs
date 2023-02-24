using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.EntityModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Repositories
{
    public class TransactionRepository : IRepository<TransactionEntity>
    {
        private readonly DbSet<TransactionEntity> _entities;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(DbSet<TransactionEntity> entities, ILogger<TransactionRepository> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(TransactionEntity item, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            var result = _entities.Add(item);
             _logger.LogTrace("Add TransactionEntity item with TransactionId:{bonusId} and TransactionProfileId:{personalId} to database", item.TransactionId, item.TransactionProfileId );
             return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<TransactionEntity> items, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(items, nameof(items));
            _entities.AddRange(items);
            _logger.LogTrace("Add TransactionEntity  to database, Count:{count}", items.Count());
            return Task.CompletedTask;
        }

        public Task Update(TransactionEntity item, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _entities.Update(item);
            _logger.LogTrace("Update TransactionEntity item with TransactionId:{bonusId} and TransactionProfileId:{personalId} in database", item.TransactionId, item.TransactionProfileId);
            return Task.CompletedTask;
        }
    }
}
