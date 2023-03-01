using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.BusinessLogic.Contracts.IServices;
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.EntityModels.Models;

namespace CashService.BusinessLogic.Services
{
    // TODO: Remove all empty lines
    public class CashTransferService : ICashService
    {
        private readonly IRepository<TransactionEntity> _transactionEntityRepository;
        private readonly IRepository<TransactionProfileEntity> _transactionProfileRepository;
        private readonly ICashProvider _cashProvider;

        // TODO: unused variables _transactionEntityProvider, _transactionProfileProvider
        private readonly IProvider<TransactionEntity> _transactionEntityProvider;
        private readonly IProvider<TransactionProfileEntity> _transactionProfileProvider;

        private readonly IDataContext _context;

        public CashTransferService(
            IRepository<TransactionEntity> transactionEntityRepository,
            IRepository<TransactionProfileEntity> transactionProfileRepository,
            ICashProvider cashProvider,
            IProvider<TransactionEntity> transactionEntityProvider,
            IProvider<TransactionProfileEntity> transactionProfileProvider,
            IDataContext context)
        {
            _transactionEntityRepository=transactionEntityRepository;
            _transactionProfileRepository=transactionProfileRepository;
            _cashProvider=cashProvider;

            _transactionEntityProvider = transactionEntityProvider;
            _transactionProfileProvider = transactionProfileProvider;

            _context=context;
        }

        // TODO: typo in profileid. Should be profileId
        public async Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileid, token);
            return balance;
        }

        // TODO: typo in profileid. Should be profileId
        public async Task<TransactionProfileEntity> CalcBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.CalcBalance(profileid, token);
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
            // TODO: typo in profileid. Should be profileId
            var profileid = withdrawTransactionProfile.ProfileId;

            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileid, token);

            if (balance is not null)
            {
                balance = await _cashProvider.CalcBalance(profileid, token);

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
                balance = await _cashProvider.CalcBalance(profileid, token);
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
