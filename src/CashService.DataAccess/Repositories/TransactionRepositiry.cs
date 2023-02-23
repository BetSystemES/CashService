using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.EntityModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Repositories
{
    public class TransactionRepositiry : IRepository<TransactionEntity>
    {
        private readonly DbSet<TransactionEntity> _entities;
        private readonly ILogger<TransactionRepositiry> _logger;

        public TransactionRepositiry(DbSet<TransactionEntity> entities, ILogger<TransactionRepositiry> logger)
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


        public async Task<TransactionEntity> Get(Guid guid, CancellationToken cancellationToken)
        {
              var result=await _entities.FindAsync(guid);
              _logger.LogTrace("Get TransactionEntity item from database by Id:{guid}", guid);
              return result;
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
