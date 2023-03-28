using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Services
{
    public interface ICashService
    {
        Task<ProfileEntity> GetBalance(Guid profileId, CancellationToken token);
        Task<ProfileEntity> CalcBalanceWithinCashtype(Guid profileId, CancellationToken token);
        Task Deposit(ProfileEntity depositProfile, CancellationToken token);
        Task<ProfileEntity> Withdraw(ProfileEntity withdrawProfile, CancellationToken token);
        Task DepositRange(List<ProfileEntity> depositRangeProfileEntities, CancellationToken token);
        Task<List<ProfileEntity>> WithdrawRange(List<ProfileEntity> withdrawRangeProfileEntities, CancellationToken token);
    }
}