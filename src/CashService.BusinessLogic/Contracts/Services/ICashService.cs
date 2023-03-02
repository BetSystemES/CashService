using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Services
{
    public interface ICashService
    {
        Task<TransactionProfileEntity> GetBalance(Guid profileId, CancellationToken token);
        Task<TransactionProfileEntity> CalcBalance(Guid profileId, CancellationToken token);
        Task Deposit(TransactionProfileEntity depositTransactionProfile, CancellationToken token);
        Task<TransactionProfileEntity> Withdraw(TransactionProfileEntity withdrawTransactionProfile, CancellationToken token);
        Task DepositRange(List<TransactionProfileEntity> depositRangeTransactionProfileEntities, CancellationToken token);
        Task<List<TransactionProfileEntity>> WithdrawRange(List<TransactionProfileEntity> withdrawRangeTransactionProfileEntities, CancellationToken token);
    }
}