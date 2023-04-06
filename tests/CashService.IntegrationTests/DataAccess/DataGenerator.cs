using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;
using FizzWare.NBuilder;

namespace CashService.IntegrationTests.DataAccess
{
    public static class DataGenerator
    {
        public static ProfileEntity GenerateEmptyProfileEntity(Guid profileId)
        {
            var expectedResult = Builder<ProfileEntity>
                .CreateNew()
                .With(x => x.Id = profileId)
                .With(x=>x.CashAmount = 0)
                .With(x=>x.RowVersion = 0)
                .Build();

            return expectedResult;
        }

        public static ProfileEntity GenerateProfileEntity(Guid profileId, decimal cashAmount,
            decimal bonusAmount)
        {
            var transaction1 = GenerateCashTransactionEntity(profileId, cashAmount);

            var transaction2 = GenerateBonusTransactionEntity(profileId, bonusAmount);

            var expectedResult = Builder<ProfileEntity>
                .CreateNew()
                .With(x => x.Id = profileId)
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
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId = profileId)
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = bonusAmount)
                .Build();
            return transaction2;
        }

        private static TransactionEntity GenerateCashTransactionEntity(Guid profileId, decimal cashAmount)
        {
            var transaction1 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId = profileId)
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = cashAmount)
                .Build();
            return transaction1;
        }

        public static ProfileEntity GenerateCashProfileEntity(Guid profileId, decimal cashAmount, decimal cashAmount2)
        {
            var transaction1 = GenerateCashTransactionEntity(profileId, cashAmount);

            var transaction2 = GenerateCashTransactionEntity(profileId, cashAmount2);

            var expectedResult = CreateProfileEntityWith2Transactions(profileId, transaction1, transaction2);

            return expectedResult;
        }

        private static ProfileEntity CreateProfileEntityWith2Transactions(Guid profileId, TransactionEntity transaction1,
            TransactionEntity transaction2)
        {
            var expectedResult = Builder<ProfileEntity>
                .CreateNew()
                .With(x => x.Id = profileId)
                .With(x => x.CashAmount = 0)
                .With(x => x.RowVersion = 0)
                .With(x => x.Transactions = new List<TransactionEntity>())
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();
            return expectedResult;
        }

        public static ProfileEntity GenerateBonusProfileEntity(Guid profileId, decimal bonusAmount, decimal bonusAmount2)
        {
            var transaction1 = GenerateBonusTransactionEntity(profileId, bonusAmount);

            var transaction2 = GenerateBonusTransactionEntity(profileId, bonusAmount2);

            var expectedResult = CreateProfileEntityWith2Transactions(profileId, transaction1, transaction2);

            return expectedResult;
        }

        public static ProfileEntity GenerateCashProfileEntity(Guid profileId, decimal cashAmount)
        {
            var transaction = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId = profileId)
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = cashAmount)
                .Build();

            var expectedResult = Builder<ProfileEntity>
                .CreateNew()
                .With(x => x.Id = profileId)
                .With(x => x.CashAmount = cashAmount)
                .With(x => x.Transactions = new List<TransactionEntity>())
                .Do(x => x.Transactions.Add(transaction))
                .Build();

            return expectedResult;
        }
    }
}