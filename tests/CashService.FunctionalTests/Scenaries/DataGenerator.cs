using CashService.GRPC;
using FizzWare.NBuilder;

namespace CashService.FunctionalTests.Scenaries
{
    public static class DataGenerator
    {
        public static TransactionModel TransactionModelGenerator(string profileId, double cashAmout, double bonusAmount)
        {
            var transaction1 = Builder<Transaction>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid().ToString())
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = cashAmout)
                .Build();
            var transaction2 = Builder<Transaction>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid().ToString())
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = bonusAmount)
                .Build();
            TransactionModel transactionModel = Builder<TransactionModel>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();
            return transactionModel;
        }

        public static TransactionRequestModel TransactionRequestModelGenerator(string profileId, double cashAmout, double bonusAmount)
        {
            var transaction1 = Builder<Transaction>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid().ToString())
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = cashAmout)
                .Build();
            var transaction2 = Builder<Transaction>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid().ToString())
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = bonusAmount)
                .Build();
            TransactionRequestModel transactionRequestModel = Builder<TransactionRequestModel>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();
            return transactionRequestModel;
        }
    }
}
