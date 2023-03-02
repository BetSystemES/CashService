using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface ICashProvider
    {
        Task<TransactionProfileEntity> GetBalance(Guid profileId, CancellationToken token);

        Task<TransactionProfileEntity> CalcBalance(Guid profileId, CancellationToken token);
    }
}
