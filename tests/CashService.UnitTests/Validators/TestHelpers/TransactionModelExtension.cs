// TODO: remove unused usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.GRPC;
using Google.Protobuf.Collections;

// TODO: wrong NameSpace. CashService.UnitTests.Validators.TestHelpers
namespace CashService.UnitTests.Validators
{
    public static class TransactionModelExtension
    {
        // TODO: typo in profileid. Should be profileId
        public static TransactionModel Init(this TransactionModel transactionModel,
            string profileid,
            RepeatedField<Transaction> transactions
         )
        {
            transactionModel.Profileid = profileid;
            transactionModel.Transactions.AddRange(transactions);
            return transactionModel;
        }
    }
}
