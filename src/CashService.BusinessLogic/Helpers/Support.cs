using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;

namespace CashService.BusinessLogic.Helpers
{
    public static class Support
    {
        public static ProfileEntity GenarateEmptyBalance(Guid profileId)
        {
            ProfileEntity profile = new()
            {
                Id = profileId,
                Transactions = new List<TransactionEntity>()
            };

            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType == 0) continue;
                TransactionEntity transactionEntity = new()
                {
                    CashType = cashType,
                    Id = Guid.NewGuid(),
                    ProfileId = profileId,
                    ProfileEntity = profile,
                    Amount = 0
                };
                profile.Transactions.Add(transactionEntity);
            }

            return profile;
        }
    }
}