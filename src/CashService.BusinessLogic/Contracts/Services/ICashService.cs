using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Criterias;
using CashService.BusinessLogic.Models;

namespace CashService.BusinessLogic.Contracts.Services
{
    public interface ICashService
    {
        Task<ProfileEntity> GetTransactionsHistory(Guid profileId, CancellationToken token);
        Task<decimal> GetBalance(Guid profileId, CancellationToken token);
        Task<ProfileEntity> CalcBalanceWithinCashtype(Guid profileId, CancellationToken token);
        Task<ProfileEntity> Deposit(ProfileEntity depositProfile, CancellationToken token);
        Task<ProfileEntity> Withdraw(ProfileEntity withdrawProfile, CancellationToken token);
        Task<List<ProfileEntity>> DepositRange(List<ProfileEntity> depositRangeProfileEntities, CancellationToken token);
        Task<List<ProfileEntity>> WithdrawRange(List<ProfileEntity> withdrawRangeProfileEntities, CancellationToken token);
        Task<PagedResponse<TransactionEntity>> GetPagedTransactions(FilterCriteria filterCriteria, CancellationToken token);
    }
}