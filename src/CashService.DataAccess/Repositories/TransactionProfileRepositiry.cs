using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Repositories
{
    public class TransactionProfileRepositiry : IRepository<TransactionProfileEntity>
    {
        private readonly DbSet<TransactionProfileEntity> _entities;
        private readonly ILogger<TransactionProfileRepositiry> _logger;

        public TransactionProfileRepositiry(DbSet<TransactionProfileEntity> entities, ILogger<TransactionProfileRepositiry> logger )
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(TransactionProfileEntity item, CancellationToken token)
        {
            _entities.Add(item);
            _logger.LogTrace("Add TransactionProfileEntity item with ProfileId:{guid} to database", item.ProfileId );
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<TransactionProfileEntity> items, CancellationToken token)
        {
            _entities.AddRange(items);
            _logger.LogTrace("Add TransactionProfileEntitis to database, Count:{count}", items.Count());
            return Task.CompletedTask;
        }

        public async Task<TransactionProfileEntity> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get TransactionProfileEntity item from database by ProfileId:{guid}", guid);
            return result;
        }

        public Task Update(TransactionProfileEntity item, CancellationToken cancellationToken)
        {
            _entities.Update(item);
            _logger.LogTrace("Update TransactionProfileEntity item  with ProfileId:{guid} in database", item.ProfileId);
            return Task.CompletedTask;
        }
    }
}