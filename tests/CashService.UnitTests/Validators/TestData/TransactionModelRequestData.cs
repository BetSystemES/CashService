using CashService.GRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;

// TODO: wrong namespace name CashService.UnitTests.Validators. Should be CashService.UnitTests.Validators.TestData
namespace CashService.UnitTests.Validators
{
   public static class TransactionModelRequestData
    {
        public static IEnumerable<object[]> TransactionModelRequestDataValid()
        {
            // TODO: use NBuilder library for data preparation
            yield return new object[]
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
               )
            };
        }

        public static IEnumerable<object[]> TransactionModelRequestDataInvalid()
        {
            // TODO: use NBuilder library for data preparation
            yield return new object[]
            {
                new TransactionModel().Init
                (
                    profileid: "",
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
                )
            };

            yield return new object[]
            {
                new TransactionModel().Init
                (
                    profileid: "34c92d2c-1f47-4a04-bffa",
                    transactions: new RepeatedField<Transaction>()
                    {
                        new Transaction().Init(
                            transactioId: "" ,
                            cashType: CashType.Cash ,
                            amount: 100
                        ),
                        new Transaction().Init(
                            transactioId: "a73fd2b1-33f3-4241-b7fd-fa4c02a2508e" ,
                            cashType: CashType.Bonus ,
                            amount: 50
                        )
                    }
                )
            };

            yield return new object[]
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
                            transactioId: "" ,
                            cashType: CashType.Bonus ,
                            amount: 50
                        )
                    }
                )
            };

            yield return new object[]
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
                )
            };

            yield return new object[]
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
                )
            };

            yield return new object[]
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
                            transactioId: "a73fd2b1-33f3-4241-b7fd" ,
                            cashType: CashType.Bonus ,
                            amount: 50
                        )
                    }
                )
            };

            yield return new object[]
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
                )
            };

            yield return new object[]
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
                            cashType: CashType.Unspecified ,
                            amount: 50
                        )
                    }
                )
            };

        }
    }
}
