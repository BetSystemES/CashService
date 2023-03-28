using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Extensions
{
    public static class ProfileEntityCashExtension
    {
        public static void ReCalcBalanceAndWithDraw(this ProfileEntity profile, List<TransactionEntity> differenceList,
         ProfileEntity balance)
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
                        profile.Transactions.FirstOrDefault(x => x.CashType == transaction.CashType);
                    if (withdrawTransaction is not null)
                    {
                        withdrawTransaction.Amount -= transaction.Amount;
                    }
                }
            }
        }

        public static List<TransactionEntity> DifferenceTransaction(this ProfileEntity profile,
            ProfileEntity balance)
        {
            List<TransactionEntity> differenceList = new();

            foreach (var withdrawTransaction in profile.Transactions)
            {
                var currentCashType = withdrawTransaction.CashType;
                //Find balance with the same CashType
                var currentBalanceTransaction = balance.Transactions.FirstOrDefault(x => x.CashType == currentCashType);

                TransactionEntity difference = new ()
                {
                    CashType = currentCashType,
                };

                decimal differenceAmount = currentBalanceTransaction is not null
                    ? currentBalanceTransaction.Amount + withdrawTransaction.Amount
                    : withdrawTransaction.Amount;

                difference.Amount = differenceAmount;
                differenceList.Add(difference);
            }

            return differenceList;
        }

        public static void CheckForUnite(this ProfileEntity profile)
        {
            bool isNeedTransactionsUnite = profile.IsNeedTransactionsUnite();
            if (isNeedTransactionsUnite) profile.UniteTransactions();
        }

        public static void UniteTransactions(this ProfileEntity profile)
        {
            ProfileEntity uniteProfile = new ()
            {
                Id = profile.Id,
                Transactions = new List<TransactionEntity>()
            };

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    var result = profile
                            .Transactions.Where(t => t.CashType == cashType)
                        .Sum(transaction => transaction.Amount);

                    uniteProfile.Transactions.Add(
                        new TransactionEntity()
                        {
                            Id = Guid.NewGuid(),
                            ProfileId = profile.Id,
                            ProfileEntity = uniteProfile,
                            CashType = cashType,
                            Amount = result,
                        }
                    );
                }
            }

            profile = uniteProfile;
        }

        public static bool IsNeedTransactionsUnite(this ProfileEntity profile)
        {
            List<int> countOfCurrentCashType = new();

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    countOfCurrentCashType.Add(profile.Transactions.Count(el => el.CashType == cashType));
                }
            }

            return countOfCurrentCashType.Any(x => x > 1) ? true : false;
        }

        public static void CalsIntersectionByCashType(this ProfileEntity profile, ProfileEntity withdrawProfile)
        {
            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    var wFind = withdrawProfile.Transactions.FirstOrDefault(x => x.CashType == cashType);

                    if (wFind is null)
                    {
                        var bFind = profile.Transactions.FirstOrDefault(x => x.CashType == cashType);
                        if (bFind is not null)
                        {
                            profile.Transactions.Remove(bFind);
                        }
                    }
                }
            }
        }
    }
}