using CashService.GRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;

namespace CashService.UnitTests.Validators
{
   public static class RepeatedTransactionModelRequestData
    {
        public static IEnumerable<object[]> RepeatedTransactionModelRequestDataValid()
        {
            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };
        }

        public static IEnumerable<object[]> RepeatedTransactionModelRequestDataInvalid()
        {
            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Unspecified ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Bonus ,
                                amount: 30
                            )
                        }
                    )
                }
            };

            yield return new object[]
            {
                new RepeatedField<TransactionModel>()
                {
                    new TransactionModel().Init
                    (
                        profileid: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "f54ed206-3c36-48f4-ae06-a63d07ace692" ,
                                cashType: CashType.Cash ,
                                amount: 100
                            ),
                            new Transaction().Init(
                                transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                                cashType: CashType.Bonus ,
                                amount: 50
                            )
                        }
                    ),
                    new TransactionModel().Init
                    (
                        profileid: "ad67de7f-cb81-4913-9c41-8c60d960d62b",
                        transactions: new RepeatedField<Transaction>()
                        {
                            new Transaction().Init(
                                transactioId: "ee460cb4-4d6a-4f8b-8d92-b9bb77ac5c12" ,
                                cashType: CashType.Cash ,
                                amount: 150
                            ),
                            new Transaction().Init(
                                transactioId: "cc081bc1-70a6-4382-be1b-44e31c12121b" ,
                                cashType: CashType.Unspecified ,
                                amount: 30
                            )
                        }
                    )
                }
            };
        }
    }
}
