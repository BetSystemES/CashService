using CashService.EntityModels.Models;

namespace CashService.BusinessLogic.Contracts.IServices
{
    // TODO: change folder name from IServices to Services
    public interface ICashService
    {
        // TODO: typo in profileid. Should be profileId
        Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token);
        // TODO: typo in profileid. Should be profileId
        Task<TransactionProfileEntity> CalcBalance(Guid profileid, CancellationToken token);
        Task Deposit(TransactionProfileEntity depositTransactionProfile, CancellationToken token);
        Task<TransactionProfileEntity> Withdraw(TransactionProfileEntity withdrawTransactionProfile, CancellationToken token);
        Task DepositRange(List<TransactionProfileEntity> depositRangeTransactionProfileEntities, CancellationToken token);
        Task<List<TransactionProfileEntity>> WithdrawRange(List<TransactionProfileEntity> withdrawRangeTransactionProfileEntities, CancellationToken token);
    }
}