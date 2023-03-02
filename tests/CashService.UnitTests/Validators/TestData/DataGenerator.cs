using CashService.GRPC;
using FizzWare.NBuilder;

namespace CashService.UnitTests.Validators.TestData
{
    public static class DataGenerator
    {
        public static TransactionModel TransactionModelGenerator(string profileId, double cashAmount, double bonusAmount)
        {
            var transaction1 = GenerateTransaction(Guid.NewGuid().ToString(), CashType.Cash, cashAmount);
            var transaction2 = GenerateTransaction(Guid.NewGuid().ToString(), CashType.Bonus, bonusAmount);
            TransactionModel transactionModel = Builder<TransactionModel>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();
            return transactionModel;
        }

        public static Transaction GenerateCashTransaction(double cashAmount)
        {
            return GenerateTransaction(Guid.NewGuid().ToString(), CashType.Cash, cashAmount);
        }
        public static Transaction GenerateBonusTransaction(double bonusAmount)
        {
            return GenerateTransaction(Guid.NewGuid().ToString(), CashType.Bonus, bonusAmount);
        }

        public static Transaction GenerateTransaction(string transactionId, CashType cashType, double amount)
        {
            var transaction = Builder<Transaction>
                .CreateNew()
                .With(x => x.TransactionId = transactionId)
                .With(x => x.CashType = cashType)
                .And(x => x.Amount = amount)
                .Build();
            return transaction;
        }

        public static TransactionModel TransactionModelGenerator(string profileId, Transaction transaction1, Transaction transaction2)
        {
            TransactionModel transactionModel = Builder<TransactionModel>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .Do(x => x.Transactions.Add(transaction1))
                .And(x => x.Transactions.Add(transaction2))
                .Build();
            return transactionModel;
        }

    }
}
