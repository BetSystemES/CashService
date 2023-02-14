using CashService.GRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.FunctionalTests.Scenaries
{
    public static class DataGenerator
    {
        public static TransactionModel TransactionModelGenerator(string profileId, double cashAmout, double bonusAmount)
        {
            TransactionModel transactionModel = new();

            Transaction transaction1 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Cash,
                Amount = cashAmout,
            };
            Transaction transaction2 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Bonus,
                Amount = bonusAmount,
            };

            transactionModel.Profileid = profileId;
            transactionModel.Transactions.Add(transaction1);
            transactionModel.Transactions.Add(transaction2);
            return transactionModel;
        }
    }
}
