using CashService.GRPC;
using Google.Protobuf.Collections;
using static CashService.UnitTests.Validators.TestData.DataGenerator;


namespace CashService.UnitTests.Validators.TestData
{
   public static class RepeatedTransactionModelRequestData
    {
        public static IEnumerable<object[]> RepeatedTransactionModelRequestDataValid()
        {
            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d", 100,50),
                    TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b", 150,30)
                }
            };
        }

        public static IEnumerable<object[]> RepeatedTransactionModelRequestDataInvalid()
        {
            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    TransactionModelGenerator("34c92d2c-1f47-4a04-bffa", 100,50),
                    TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b", 150,30)
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d", 100,50),
                    TransactionModelGenerator("ad67de7f-cb81-4913-9c41", 150,30)
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                        GenerateTransaction("a73fd2b1-33f3-4241-b7fd", CashType.Cash, 100),
                        GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Bonus, 50)),
                    
                    TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        GenerateTransaction("ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12", CashType.Cash, 100),
                        GenerateTransaction("cc081bc1-70a6-4382-be1b-44e31c12121b", CashType.Bonus, 50))
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                        GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Cash, 100),
                        GenerateTransaction("f54ed206-3c36-48f4-ae06", CashType.Bonus, 50)),

                    TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        GenerateTransaction("ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12", CashType.Cash, 100),
                        GenerateTransaction("cc081bc1-70a6-4382-be1b-44e31c12121b", CashType.Bonus, 50))
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Cash, 100),
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Bonus, 50)),

                TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b",
                    GenerateTransaction("ee460cb4-4d6a-4f8b-8d92", CashType.Cash, 100),
                    GenerateTransaction("cc081bc1-70a6-4382-be1b-44e31c12121b", CashType.Bonus, 50))
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Cash, 100),
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Bonus, 50)),

                TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b",
                    GenerateTransaction("ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12", CashType.Cash, 100),
                    GenerateTransaction("cc081bc1-70a6-4382-be1b", CashType.Bonus, 50))
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Unspecified, 100),
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Bonus, 50)),

                TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b",
                    GenerateTransaction("ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12", CashType.Cash, 100),
                    GenerateTransaction("cc081bc1-70a6-4382-be1b-44e31c12121b", CashType.Unspecified, 50))
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                TransactionModelGenerator("34c92d2c-1f47-4a04-bffa-71101718b56d",
                    GenerateTransaction("a73fd2b1-33f3-4241-b7fd-fa4c02a2508e", CashType.Cash, 100),
                    GenerateTransaction("f54ed206-3c36-48f4-ae06-a63d07ace692", CashType.Bonus, 50)),

                TransactionModelGenerator("ad67de7f-cb81-4913-9c41-8c60d960d62b",
                    GenerateTransaction("ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12", CashType.Cash, 100),
                    GenerateTransaction("cc081bc1-70a6-4382-be1b-44e31c12121b", CashType.Unspecified, 50))
                }
            };
        }
    }
}
