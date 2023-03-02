using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Repositories
{
    public class TransactionProfileRepository : IRepository<TransactionProfileEntity>
    {
        private readonly DbSet<TransactionProfileEntity> _entities;
        private readonly ILogger<TransactionProfileRepository> _logger;

        public TransactionProfileRepository(DbSet<TransactionProfileEntity> entities, ILogger<TransactionProfileRepository> logger )
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(TransactionProfileEntity item, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _entities.Add(item);
            _logger.LogTrace("Add TransactionProfileEntity item with ProfileId:{guid} to database", item.ProfileId );
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<TransactionProfileEntity> items, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(items, nameof(items));
            _entities.AddRange(items);
            _logger.LogTrace("Add TransactionProfileEntities to database, Count:{count}", items.Count());
            return Task.CompletedTask;
        }

        public Task Update(TransactionProfileEntity item, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _entities.Update(item);
            _logger.LogTrace("Update TransactionProfileEntity item  with ProfileId:{guid} in database", item.ProfileId);
            return Task.CompletedTask;
        }
    }
}