using CashService.EntityModels.Models;

namespace CashService.BusinessLogic.Services
{
    public static class TransactionProfileEntityCashExtension
    {
        public static void ReCalcBalanceAndWithDraw(this TransactionProfileEntity transactionProfile, List<TransactionEntity> differenceList,
         TransactionProfileEntity balance)
        {
            foreach (var transaction in differenceList)
            {
                if (transaction.Amount > 0)
                {
                    //update balance
                    var balanceTransaction = balance.Transactions.FirstOrDefault(x => x.CashType == transaction.CashType);
                    if (balanceTransaction is not null)
                    {
                        balanceTransaction.Amount -= transaction.Amount;
                    }
                }
                else
                {
                    //update withdraw
                    var withdrawTransaction =
                        transactionProfile.Transactions.FirstOrDefault(x => x.CashType == transaction.CashType);
                    if (withdrawTransaction is not null)
                    {
                        withdrawTransaction.Amount = withdrawTransaction.Amount - transaction.Amount;
                    }
                }
            }
        }

        public static List<TransactionEntity> DifferenceTransaction(this TransactionProfileEntity transactionProfile,
            TransactionProfileEntity balance)
        {
            List<TransactionEntity> differenceList = new();

            foreach (var withdrawTransaction in transactionProfile.Transactions)
            {
                var currentCashType = withdrawTransaction.CashType;
                //Find balance with the same CashType
                var currentBalanceTransaction = balance.Transactions.FirstOrDefault(x => x.CashType == currentCashType);

                TransactionEntity difference = new TransactionEntity()
                {
                    CashType = currentCashType,
                };

                decimal differenceAmount = (currentBalanceTransaction is not null)
                    ? currentBalanceTransaction.Amount + withdrawTransaction.Amount
                    : withdrawTransaction.Amount;

                difference.Amount = differenceAmount;
                differenceList.Add(difference);
            }

            return differenceList;
        }

        public static void CheckForUnite(this TransactionProfileEntity transactionProfile)
        {
            bool isNeedTransactionsUnite = IsNeedTransactionsUnite(transactionProfile);
            if (isNeedTransactionsUnite) UniteTransactions(transactionProfile);
        }

        public static void UniteTransactions(this TransactionProfileEntity transactionProfile)
        {
            TransactionProfileEntity uniteTransactionProfile = new TransactionProfileEntity()
            {
                ProfileId = transactionProfile.ProfileId,
            };

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    var result = transactionProfile
                            .Transactions.Where(t => t.CashType == cashType)
                        .Sum(transaction => transaction.Amount);

                    uniteTransactionProfile.Transactions.Add(
                        new TransactionEntity()
                        {
                            TransactionId = Guid.NewGuid(),
                            TransactionProfileId = transactionProfile.ProfileId,
                            TransactionProfileEntity = uniteTransactionProfile,
                            CashType = cashType,
                            Amount = result,
                        }
                    );
                }
            }

            transactionProfile = uniteTransactionProfile;
        }

        public static bool IsNeedTransactionsUnite(this TransactionProfileEntity transactionProfile)
        {
            List<int> countOfCurrentCashType = new();

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    countOfCurrentCashType.Add(transactionProfile.Transactions.Count(el => el.CashType == cashType));
                }
            }

            return (countOfCurrentCashType.Any(x => x>1)) ? true : false;
        }

        public static void CalsIntersectionByCashType(this TransactionProfileEntity transactionProfile, TransactionProfileEntity withdrawTransactionProfile)
        {
            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    var wFind = withdrawTransactionProfile.Transactions.FirstOrDefault(x => x.CashType == cashType);

                    if (wFind is null)
                    {
                        var bFind = transactionProfile.Transactions.FirstOrDefault(x => x.CashType == cashType);
                        if (bFind is not null)
                        {
                            transactionProfile.Transactions.Remove(bFind);
                        }
                    }
                }
            }
        }

    }
}
