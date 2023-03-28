using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Entities;
using System.Linq.Expressions;
using CashService.BusinessLogic.Extensions;

namespace CashService.DataAccess.Providers
{
    public class TransactionProvider : ITransactionProvider
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

        public async Task<int> GetCount(Expression<Func<TransactionEntity, bool>> expressionTransaction, CancellationToken token)
        {
            return await _entities.CountAsync(expressionTransaction, token);
        }

        public async Task<List<TransactionEntity>> GetPagedTransactions(
            Expression<Func<TransactionEntity, bool>> expressionTransactionEntity,
            Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>> order,
            int? skip, int? take,
            CancellationToken token)
        {
            var result = await _entities
                .Where(expressionTransactionEntity)
                .OrderByFunc(order)
                .SkipTake(skip, take)
                .ToListAsync(token);
            _logger.LogTrace("Find Transactions from database by predicate and by page with params: " +
                             "skip={skip} and take={take}. Count={Count}", skip, take, result.Count);
            return result;
        }
    }
}
