using CashService.BusinessLogic.Entities;
using System.Linq.Expressions;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface ITransactionProvider
    {
        Task<TransactionEntity> Get(Guid id, CancellationToken cancellationToken);

        Task<int> GetCount(Expression<Func<TransactionEntity, bool>> expressionTransaction, CancellationToken token);

        Task<List<TransactionEntity>> GetPagedTransactions(
            Expression<Func<TransactionEntity, bool>> expressionTransactionEntity,
            Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>> order,
            int? skip, int? take,
            CancellationToken token);
    }
}
