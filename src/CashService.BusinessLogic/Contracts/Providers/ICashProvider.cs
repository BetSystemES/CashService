using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface ICashProvider
    {
        Task<ProfileEntity> GetTransactionsHistory(Guid profileId, CancellationToken token);

        Task<ProfileEntity> CalcBalanceWithinCashtype(Guid profileId, CancellationToken token);
    }
}
