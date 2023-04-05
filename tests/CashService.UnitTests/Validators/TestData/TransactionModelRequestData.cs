using CashService.GRPC;
using static CashService.UnitTests.Validators.TestData.DataGenerator;

namespace CashService.UnitTests.Validators.TestData
{
   public static class TransactionModelRequestData
    {
        public static IEnumerable<object[]> TransactionModelRequestDataValid()
        {
            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d", 100,50),
            };
        }

        public static IEnumerable<object[]> TransactionModelRequestDataInvalid()
        {
            yield return new object[]
            {
                TransactionRequestModelGenerator("", 100,50),
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("", CashType.Cash, 100),
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Bonus, 50))
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Cash, 100),
                    GenerateTransaction("", CashType.Bonus, 50))
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa", 100,50),
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd", CashType.Cash, 100),
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Bonus, 50))
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Cash, 100),
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-", CashType.Bonus, 50))
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Unspecified, 100),
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Bonus, 50))
            };

            yield return new object[]
            {
                TransactionRequestModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Cash, 100),
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Unspecified, 50))
            };

        }
    }
}