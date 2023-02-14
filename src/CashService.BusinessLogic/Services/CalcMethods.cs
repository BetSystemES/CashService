using CashService.BusinessLogic.Contracts.IServices;
using CashService.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.BusinessLogic.Services
{
    public partial class CashTransferService : ICashService
    {
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
                        balanceTransaction.Amount -= transaction.Amount;
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
    }
}
