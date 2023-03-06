using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface ITransactionProfileProvider
    {
        Task<TransactionProfileEntity> Get(Guid id, CancellationToken cancellationToken);
    }
}
