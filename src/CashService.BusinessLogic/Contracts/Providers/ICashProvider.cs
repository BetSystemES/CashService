using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface ICashProvider
    {
        Task<ProfileEntity> GetBalance(Guid profileId, CancellationToken token);

        Task<ProfileEntity> CalcBalance(Guid profileId, CancellationToken token);
    }
}
