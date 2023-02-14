using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.BusinessLogic.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.BusinessLogic.Models;

namespace CashService.BusinessLogic.Services
{
    public partial class CashTransferService : ICashService
    {
        private readonly IRepository<TransactionEntity> _transactionEntityRepository;
        private readonly IRepository<TransactionProfileEntity> _transactionProfileRepository;
        private readonly ICashProvider _cashProvider;

        private readonly IDataContext _context;

        public CashTransferService(
            IRepository<TransactionEntity> transactionEntityRepository,
            IRepository<TransactionProfileEntity> transactionProfileRepository,
            ICashProvider cashProvider,
            IDataContext context)
        {
            _transactionEntityRepository=transactionEntityRepository;
            _transactionProfileRepository=transactionProfileRepository;
            _cashProvider=cashProvider;
            _context=context;
        }

        public async Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileid, token);
            return balance;
        }

        public async Task<TransactionProfileEntity> CalcBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.CalcBalance(profileid, token);
            return balance;
        }

        public async Task Deposit(TransactionProfileEntity depositTransactionProfile, CancellationToken token)
        {
            TransactionProfileEntity balance = await _cashProvider.GetBalance(depositTransactionProfile.ProfileId, token);

            if (balance == null)
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
            var profileid = withdrawTransactionProfile.ProfileId;

            TransactionProfileEntity balance = await _cashProvider.GetBalance(profileid, token);

            if (balance != null)
            {
                balance = await _cashProvider.CalcBalance(profileid, token);

                CheckForUnite(withdrawTransactionProfile);

                var differenceList = DifferenceTransaction(withdrawTransactionProfile, balance);

                ReCalcBalanceAndWithDraw(withdrawTransactionProfile, differenceList, balance);

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
