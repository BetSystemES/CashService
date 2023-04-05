using CashService.FunctionalTests.Adapters;
using CashService.GRPC;
using FluentAssertions;
using Newtonsoft.Json;
using NScenario;
using Xunit.Abstractions;
using static CashService.FunctionalTests.Scenaries.DataGenerator;
using static CashService.GRPC.CashService;

namespace CashService.FunctionalTests.Scenaries
{
    public class ScenarioCashServiceTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly CashServiceClient _client;

        public ScenarioCashServiceTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new CashServiceClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioGetBalance()
        {
            var profileId = Guid.NewGuid().ToString();
            var transactionModel = TransactionModelGenerator(profileId, 95, 50);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"GetBalance");

            await scenario
                .Step($"Create CashService Profile",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            var addDepositResponse = await scenario
                .Step($"Deposit",
                async () =>
                {
                    var request = new DepositRequest()
                    {
                        Deposit = transactionModel,
                    };

                    return await _client.DepositAsync(request);
                });

            var getBalanceResponse = await scenario
                .Step($"GetBalance",
                async () =>
                {
                    var request = new GetBalanceRequest()
                    {
                        ProfileId = profileId
                    };
                    return await _client.GetBalanceAsync(request);
                });

            var result = getBalanceResponse.Balance;

            result
                .Should()
                .NotBeNull()
                .Equals(transactionModel);
        }

        [Fact()]
        public async Task ScenarioWithdrawAllAfterDeposit()
        {
            var profileId = Guid.NewGuid().ToString();

            var depositCashAmount = 100;
            var depositBonusAmount = 60;

            var withdrawCashAmount = 100;
            var withdrawBonusAmount = 60;

            var expectedCashAmount = depositCashAmount - withdrawCashAmount;

            var depositTransaction = TransactionModelGenerator(
                profileId,
                depositCashAmount,
                depositBonusAmount);

            var withdrawTransaction = TransactionModelGenerator(
                profileId,
                withdrawCashAmount,
                withdrawBonusAmount);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"Withdraw all after deposit");

            await scenario
                .Step($"Create CashService Profile",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            var depositResponse = await scenario
                .Step($"Deposit",
                    async () =>
                    {
                        var request = new DepositRequest()
                        {
                            Deposit = depositTransaction
                        };

                        return await _client.DepositAsync(request);
                    });

            var withdrawResponse = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withdrawTransaction,
                        };

                        return await _client.WithdrawAsync(request);
                    });

            var getBalanceResponse = await scenario
                .Step($"GetBalance",
                async () =>
                {
                    var request = new GetBalanceRequest()
                    {
                        ProfileId = profileId
                    };
                    return await _client.GetBalanceAsync(request);
                });

            var result = withdrawResponse.Withdrawresponse;

            getBalanceResponse.Balance.CashAmount
                .Should()
                .Be(expectedCashAmount);
        }

        [Fact()]
        public async Task ScenarioWithDraw0()
        {
            var profileId = Guid.NewGuid().ToString();

            var withDrawModel = TransactionModelGenerator(profileId, 100, 60);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDraw0");

            await scenario
                .Step($"Create CashService Profile",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            var withdrawResponse = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withDrawModel,
                        };

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponse.Withdrawresponse;

            result.Transactions[0].Amount
                .Should()
                .Be(0);

            result.Transactions[1].Amount
                .Should()
                .Be(0);
        }

        [Fact()]
        public async Task ScenarioWithDraw()
        {
            var profileId = Guid.NewGuid().ToString();

            var depositTransaction = TransactionModelGenerator(profileId, 140, 50);
            var withdrawTransaction = TransactionModelGenerator(profileId, 100, 60);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDraw");

            await scenario
                .Step($"Create CashService Profile",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            var addDepositResponse = await scenario
                .Step($"Deposit",
                    async () =>
                    {
                        var request = new DepositRequest()
                        {
                            Deposit = depositTransaction,
                        };

                        return await _client.DepositAsync(request);
                    });

            var withdrawResponse = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withdrawTransaction,
                        };

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponse.Withdrawresponse;

            result.Transactions[0].Amount
                .Should()
                .Be(100);

            result.Transactions[1].Amount
                .Should()
                .Be(50);
        }

        [Fact()]
        public async Task ScenarioWithDraw2()
        {
            var profileId = Guid.NewGuid().ToString();

            var transactionModel = TransactionModelGenerator(profileId, 40, 50);

            var withDrawModel = TransactionModelGenerator(profileId, 100, 60);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDraw2");

            await scenario
                .Step($"Create CashService Profile",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            var addDepositResponse = await scenario
                .Step($"Deposit",
                    async () =>
                    {
                        var request = new DepositRequest()
                        {
                            Deposit = transactionModel,
                        };

                        return await _client.DepositAsync(request);
                    });

            var withdrawResponse = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withDrawModel,
                        };

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponse.Withdrawresponse;

            result.Transactions[0].Amount
                .Should()
                .Be(40);

            result.Transactions[1].Amount
                .Should()
                .Be(50);
        }

        [Fact()]
        public async Task ScenarioDepositRange()
        {
            // Arrange
            var profileId1 = Guid.NewGuid().ToString();
            var depositCashAmount1 = 140;
            var depositBonusAmount1 = 50;

            var depositTransaction1 = TransactionModelGenerator(
                profileId1,
                depositCashAmount1,
                depositBonusAmount1);

            var profileId2 = Guid.NewGuid().ToString();
            var depositCashAmount2 = 100;
            var depositBonusAmount2 = 60;

            var depositTransaction2 = TransactionModelGenerator(
                profileId2,
                depositCashAmount2,
                depositBonusAmount2);

            // Act
            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"DepositRange");

            await scenario
                .Step($"Create CashService Profile with userId={profileId1}",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId1,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            await scenario
               .Step($"Create CashService Profile with userId={profileId2}",
               async () =>
               {
                   var request = new CreateCashProfileRequest()
                   {
                       UserId = profileId2,
                   };

                   return await _client.CreateCashProfileAsync(request);
               });

            var addDepositRangeResponse = await scenario
                .Step($"DepositRange",
                    async () =>
                    {
                        var request = new DepositRangeRequest();

                        IEnumerable<TransactionModel> depositTransactions = new[]
                        {
                            depositTransaction1,
                            depositTransaction2
                        };

                        request.DepositRangeRequests.AddRange(depositTransactions);

                        string json = JsonConvert.SerializeObject(request).ToLower();

                        return await _client.DepositRangeAsync(request);
                    });

            var getBalanceResponse1 = await scenario
                .Step($"GetBalance1",
                    async () =>
                    {
                        var request = new GetBalanceRequest()
                        {
                            ProfileId = profileId1
                        };
                        return await _client.GetBalanceAsync(request);
                    });

            var getBalanceResponse2 = await scenario
                .Step($"GetBalance2",
                    async () =>
                    {
                        var request = new GetBalanceRequest()
                        {
                            ProfileId = profileId2
                        };
                        return await _client.GetBalanceAsync(request);
                    });

            // get balance()
            // get profile entity
            // return cash amount value

            var balance1 = getBalanceResponse1.Balance;

            var balance2 = getBalanceResponse2.Balance;

            // Assert

            balance1.Transactions.Count
                .Should()
                .Be(2);

            balance1.CashAmount
                .Should()
                .Be(depositCashAmount1);

            balance1
                .Should()
                .NotBeNull()
                .Equals(depositTransaction1);

            balance2.Transactions.Count
                .Should()
                .Be(2);

            balance2.CashAmount
                .Should()
                .Be(depositCashAmount2);

            balance2
                .Should()
                .NotBeNull()
                .Equals(depositTransaction2);
        }

        [Fact()]
        public async Task ScenarioWithDrawRange()
        {
            var profileId = Guid.NewGuid().ToString();
            var transactionModel = TransactionModelGenerator(profileId, 140, 50);

            var profileId2 = Guid.NewGuid().ToString();
            var transactionModel2 = TransactionModelGenerator(profileId2, 100, 60);

            var withDrawModel = TransactionModelGenerator(profileId, 100, 30);

            var withDrawModel2 = TransactionModelGenerator(profileId2, 50, 10);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDrawRange");

            await scenario
                .Step($"Create CashService Profile with userId={profileId}",
                async () =>
                {
                    var request = new CreateCashProfileRequest()
                    {
                        UserId = profileId,
                    };

                    return await _client.CreateCashProfileAsync(request);
                });

            await scenario
               .Step($"Create CashService Profile with userId={profileId2}",
               async () =>
               {
                   var request = new CreateCashProfileRequest()
                   {
                       UserId = profileId,
                   };

                   return await _client.CreateCashProfileAsync(request);
               });

            var addDepositRangeResponse = await scenario
                .Step($"DepositRange",
                    async () =>
                    {
                        var request = new DepositRangeRequest();

                        IEnumerable<TransactionModel> transactionModels = new[]
                        {
                            transactionModel,
                            transactionModel2
                        };

                        request.DepositRangeRequests.AddRange(transactionModels);

                        string json = JsonConvert.SerializeObject(request).ToLower();

                        return await _client.DepositRangeAsync(request);
                    });

            var withdrawRangeResponse = await scenario
                .Step($"WithDrawRange",
                    async () =>
                    {
                        var request = new WithdrawRangeRequest()
                        {
                            WithdrawRangeRequests =
                           {
                               withDrawModel,
                               withDrawModel2
                           }
                        };

                        string json = JsonConvert.SerializeObject(request).ToLower();

                        return await _client.WithdrawRangeAsync(request);
                    });

            var result = withdrawRangeResponse.WithdrawRangeResponses[0];
            var result2 = withdrawRangeResponse.WithdrawRangeResponses[1];

            result.Transactions.Count
                .Should()
                .Be(2);

            result.Transactions[0].Amount
                .Should()
                .Be(100);

            result.Transactions[1].Amount
                .Should()
                .Be(30);

            result2.Transactions.Count
                .Should()
                .Be(2);

            result2.Transactions[0].Amount
                .Should()
                .Be(50);

            result2.Transactions[1].Amount
                .Should()
                .Be(10);
        }
    }
}