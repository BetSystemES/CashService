using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.EntityModels.Models;

namespace CashService.UnitTests.Support
{
    public static class TestTransactionProfileEntityGenerator
    {
        public static TransactionProfileEntity GenerateTransactionProfile()
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
    }
}
