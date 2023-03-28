using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;
using FizzWare.NBuilder;

namespace CashService.UnitTests.Support
{
    public static class TestProfileEntityGenerator
    {
        public static ProfileEntity GenerateProfile()
        {
            var profileId = Guid.NewGuid();

            var transaction1 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId=profileId)
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = 95)
                .Build();

            var transaction2 = Builder<TransactionEntity>
                .CreateNew()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId=profileId)
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = 50)
                .Build();

            var expectedResult = Builder<ProfileEntity>
                .CreateNew()
                .With(x => x.Id = profileId)
                .With(x=>x.Transactions = new List<TransactionEntity>())
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();

            return expectedResult;
        }
    }
}
