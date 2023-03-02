using CashService.GRPC;
using FizzWare.NBuilder;

namespace CashService.FunctionalTests.Scenaries
{
    public static class DataGenerator
    {
        public static TransactionModel TransactionModelGenerator0(string profileId, double cashAmout, double bonusAmount)
        {
            TransactionModel transactionModel = new();
            Transaction transaction1 = new()
            {
                TransactionId = Guid.NewGuid().ToString(),
                CashType = CashType.Cash,
                Amount = cashAmout,
            };
            Transaction transaction2 = new()
            {
                TransactionId = Guid.NewGuid().ToString(),
                CashType = CashType.Bonus,
                Amount = bonusAmount,
            };
            transactionModel.ProfileId = profileId;
            transactionModel.Transactions.Add(transaction1);
            transactionModel.Transactions.Add(transaction2);
            return transactionModel;
        }

        public static TransactionModel TransactionModelGenerator(string profileId, double cashAmout, double bonusAmount)
        {
            var transaction1 = Builder<Transaction>
                .CreateNew()
                .With(x=>x.TransactionId = Guid.NewGuid().ToString())
                .With(x => x.CashType = CashType.Cash)
                .And(x => x.Amount = cashAmout)
                .Build();
            var transaction2 = Builder<Transaction>
                .CreateNew()
                .With(x => x.TransactionId = Guid.NewGuid().ToString())
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = bonusAmount)
                .Build();
            TransactionModel transactionModel =  Builder<TransactionModel>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();
            return transactionModel;
        }
    }
}
