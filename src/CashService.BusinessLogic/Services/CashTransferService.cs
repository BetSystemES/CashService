using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Extensions;

namespace CashService.BusinessLogic.Services
{
    public class CashTransferService : ICashService
    {
        private readonly ITransactionRepository _transactionEntityRepository;
        private readonly ITransactionProfileRepository _transactionProfileRepository;
        private readonly ICashProvider _cashProvider;

        private readonly ITransactionProvider _transactionEntityProvider;
        private readonly ITransactionProfileProvider _transactionProfileProvider;

        private readonly IDataContext _context;

        public CashTransferService(
            ITransactionRepository transactionEntityRepository,
            ITransactionProfileRepository transactionProfileRepository,
            ICashProvider cashProvider,
            ITransactionProvider transactionEntityProvider,
            ITransactionProfileProvider transactionProfileProvider,
            IDataContext context)
        {
            _transactionEntityRepository=transactionEntityRepository;
            _transactionProfileRepository=transactionProfileRepository;
            _cashProvider=cashProvider;

            _transactionEntityProvider = transactionEntityProvider;
            _transactionProfileProvider = transactionProfileProvider;

            _context=context;
        }

        public async Task<TransactionProfileEntity> GetBalance(Guid profileId, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileId, token);
            return balance;
        }

        public async Task<TransactionProfileEntity> CalcBalance(Guid profileId, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.CalcBalance(profileId, token);
            return balance;
        }

        public async Task Deposit(TransactionProfileEntity depositTransactionProfile, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.GetBalance(depositTransactionProfile.ProfileId, token);

            if (balance is null)
            {
                await _transactionProfileRepository.Add(depositTransactionProfile, token);
            }
            else
            {
                await _transactionEntityRepository.AddRange(depositTransactionProfile.Transactions, token);
            }
            await _context.SaveChanges(token);
        }

        public async Task<TransactionProfileEntity> Withdraw(TransactionProfileEntity withdrawTransactionProfile, CancellationToken token)
        {
            var profileId = withdrawTransactionProfile.ProfileId;

            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileId, token);

            if (balance is not null)
            {
                balance = await _cashProvider.CalcBalance(profileId, token);

                withdrawTransactionProfile.CheckForUnite();

                balance.CalsIntersectionByCashType(withdrawTransactionProfile);

                var differenceList = withdrawTransactionProfile.DifferenceTransaction(balance);

                withdrawTransactionProfile.ReCalcBalanceAndWithDraw(differenceList, balance);

                //await _transactionProfileRepository.Add(withdrawTransactionProfile, token);
                await _transactionEntityRepository.AddRange(withdrawTransactionProfile.Transactions, token);
                await _context.SaveChanges(token);
            }
            else
            {
                balance = await _cashProvider.CalcBalance(profileId, token);
            }

            return balance;
        }

        public async Task DepositRange(List<TransactionProfileEntity> depositRangeTransactionProfileEntities, CancellationToken token)
        {
            foreach (var transactionProfileEntity in depositRangeTransactionProfileEntities)
            {
               await Deposit(transactionProfileEntity, token);
            }
        }

        public async Task<List<TransactionProfileEntity>> WithdrawRange(List<TransactionProfileEntity> withdrawRangeTransactionProfileEntities, CancellationToken token)
        {
            List<TransactionProfileEntity> transactionProfileEntities = new();
            foreach (var transactionProfileEntity in withdrawRangeTransactionProfileEntities)
            {
               var result = await Withdraw(transactionProfileEntity, token);
               transactionProfileEntities.Add(result);
            }
            return transactionProfileEntities;
        }
    }
}