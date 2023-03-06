using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface ITransactionProvider
    {
        Task<TransactionEntity> Get(Guid id, CancellationToken cancellationToken);
    }
}
