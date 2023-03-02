using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Entities;

namespace CashService.DataAccess.Providers
{
    public class TransactionProvider : IProvider<TransactionEntity>
    {
        private readonly DbSet<TransactionEntity> _entities;

        private readonly ILogger<TransactionProvider> _logger;

        public TransactionProvider(DbSet<TransactionEntity> entities,
            ILogger<TransactionProvider> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<TransactionEntity> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get TransactionEntity item from database by Id:{guid}", guid);
            return result;
        }
    }
}
