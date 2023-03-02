using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;
using FizzWare.NBuilder;

namespace CashService.UnitTests.Support
{
    public static class TestTransactionProfileEntityGenerator
    {
        public static TransactionProfileEntity GenerateTransactionProfile0()
        {
            var profileId = Guid.NewGuid();

            TransactionProfileEntity expectedResult = new TransactionProfileEntity();

            TransactionEntity transaction1 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                //TransactionProfileEntity = expectedResult,
                CashType = CashType.Cash,
                Amount = 95,
            };
            TransactionEntity transaction2 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                //TransactionProfileEntity = expectedResult,
                CashType = CashType.Bonus,
                Amount = 50,
            };

            expectedResult.ProfileId = profileId;
            expectedResult.Transactions = new List<TransactionEntity>()
            {
                transaction1,
                transaction2
            };

            return expectedResult;
        }
        public static TransactionProfileEntity GenerateTransactionProfile()
        {
            var profileId = Guid.NewGuid();

            var transaction1 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid())
                .With(x => x.TransactionProfileId=profileId)
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = 95)
                .Build();

            var transaction2 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid())
                .With(x => x.TransactionProfileId=profileId)
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = 50)
                .Build();

            var expectedResult = Builder<TransactionProfileEntity>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .With(x=>x.Transactions = new List<TransactionEntity>())
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();

            return expectedResult;
        }
    }
}
