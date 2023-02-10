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
    public class CashTransferService : ICashService
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

            TransactionProfileEntity balance = await _cashProvider.CalcBalance(profileid, token);

            if (balance != null)
            {
                CheckForUnite(withdrawTransactionProfile);

                var differenceList = DifferenceTransaction(withdrawTransactionProfile, balance);

                ReCalcBalanceAndWithDraw(withdrawTransactionProfile, differenceList, balance);

                //await _transactionProfileRepository.Add(withdrawTransactionProfile, token);
                await _transactionEntityRepository.AddRange(withdrawTransactionProfile.Transactions, token);
                await _context.SaveChanges(token);
            }

            return balance;
        }

        private static void ReCalcBalanceAndWithDraw(TransactionProfileEntity withdrawTransactionProfile, List<TransactionEntity> differenceList,
            TransactionProfileEntity balance)
        {
            foreach (var transaction in differenceList)
            {
                if (transaction.Amount > 0)
                {
                    //update balance
                    var balanceTransaction = balance.Transactions.FirstOrDefault(x => x.CashType == transaction.CashType);
                    if (balanceTransaction != null)
                    {
                        balanceTransaction.Amount = transaction.Amount;
                    }
                }
                else
                {
                    //update withdraw
                    var withdrawTransaction =
                        withdrawTransactionProfile.Transactions.FirstOrDefault(x => x.CashType == transaction.CashType);
                    if (withdrawTransaction != null)
                    {
                        withdrawTransaction.Amount = withdrawTransaction.Amount - transaction.Amount;
                    }
                }
            }
        }

        private static List<TransactionEntity> DifferenceTransaction(TransactionProfileEntity withdrawTransactionProfile,
            TransactionProfileEntity balance)
        {
            List<TransactionEntity> differenceList = new();

            foreach (var withdrawTransaction in withdrawTransactionProfile.Transactions)
            {
                var currentCashType = withdrawTransaction.CashType;
                //Find balance with the same CashType
                var currentBalanceTransaction = balance.Transactions.FirstOrDefault(x => x.CashType == currentCashType);

                TransactionEntity difference = new TransactionEntity()
                {
                    CashType = currentCashType,
                };

                decimal differenceAmount = (currentBalanceTransaction != null)
                    ? currentBalanceTransaction.Amount + withdrawTransaction.Amount
                    : withdrawTransaction.Amount;

                difference.Amount = differenceAmount;
                differenceList.Add(difference);
            }

            return differenceList;
        }

        private void CheckForUnite(TransactionProfileEntity withdrawTransactionProfile)
        {
            bool isNeedTransactionsUnite = IsNeedTransactionsUnite(withdrawTransactionProfile);
            if (isNeedTransactionsUnite) UniteTransactions(withdrawTransactionProfile);
        }

        private void UniteTransactions(TransactionProfileEntity withdrawTransactionProfile)
        {
            TransactionProfileEntity uniteTransactionProfile = new TransactionProfileEntity()
            {
                ProfileId = withdrawTransactionProfile.ProfileId,
            };

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    var result = withdrawTransactionProfile
                            .Transactions.Where(t => t.CashType == cashType)
                        .Sum(transaction => transaction.Amount);

                    uniteTransactionProfile.Transactions.Add(
                        new TransactionEntity()
                        {
                            TransactionId = Guid.NewGuid(),
                            TransactionProfileId = withdrawTransactionProfile.ProfileId,
                            TransactionProfileEntity = uniteTransactionProfile,
                            CashType = cashType,
                            Amount = result,
                        }
                    );
                }
            }

            withdrawTransactionProfile = uniteTransactionProfile;
        }

        private bool IsNeedTransactionsUnite(TransactionProfileEntity withdrawTransactionProfile)
        {
            List<int> countOfCurrentCashType = new();

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    countOfCurrentCashType.Add(withdrawTransactionProfile.Transactions.Count(el => el.CashType == cashType));
                }
            }

            return (countOfCurrentCashType.Any(x => x>1)) ? true : false;
        }

        public Task DepositRange(List<TransactionProfileEntity> depositRangeTransactionProfileEntities, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransactionProfileEntity>> WithdrawRange(List<TransactionProfileEntity> withdrawRangeTransactionProfileEntities, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
