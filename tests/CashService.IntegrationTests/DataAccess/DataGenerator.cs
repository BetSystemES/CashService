using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;
using FizzWare.NBuilder;

namespace CashService.IntegrationTests.DataAccess
{
    public static class DataGenerator
    {
        public static TransactionProfileEntity GenerateTransactionProfileEntity(Guid profileId, decimal cashAmount,
            decimal bonusAmount)
        {
            var transaction1 = GenerateCashTransactionEntity(profileId, cashAmount);

            var transaction2 = GenerateBonusTransactionEntity(profileId, bonusAmount);

            var expectedResult = Builder<TransactionProfileEntity>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .With(x => x.Transactions = new List<TransactionEntity>())
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();

            return expectedResult;
        }

        private static TransactionEntity GenerateBonusTransactionEntity(Guid profileId, decimal bonusAmount)
        {
            var transaction2 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid())
                .With(x => x.TransactionProfileId = profileId)
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = bonusAmount)
                .Build();
            return transaction2;
        }

        private static TransactionEntity GenerateCashTransactionEntity(Guid profileId, decimal cashAmount)
        {
            var transaction1 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid())
                .With(x => x.TransactionProfileId = profileId)
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = cashAmount)
                .Build();
            return transaction1;
        }

        public static TransactionProfileEntity GenerateCashProfileEntity(Guid profileId, decimal cashAmount,
            decimal cashAmount2)
        {
            var transaction1 = GenerateCashTransactionEntity(profileId, cashAmount);

            var transaction2 = GenerateCashTransactionEntity(profileId, cashAmount2);

            var expectedResult = Builder<TransactionProfileEntity>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .With(x => x.Transactions = new List<TransactionEntity>())
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();

            return expectedResult;
        }
    }
}