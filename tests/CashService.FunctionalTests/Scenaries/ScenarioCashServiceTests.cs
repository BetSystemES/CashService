using AutoMapper;
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
        public async Task ScenarioGetBalanceAfterDeposit()
        {
            var depositCashAmount = 95;
            var depositBonusAmount = 50;
            var profileId = Guid.NewGuid().ToString();

            var depositTransaction = TransactionRequestModelGenerator(
                profileId,
                depositCashAmount,
                depositBonusAmount);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"GetBalanceAfterDeposit");

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

            var cashBalance = getBalanceResponse.Balance;

            cashBalance.Should().Be(depositCashAmount);
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

            var depositTransaction = TransactionRequestModelGenerator(
                profileId,
                depositCashAmount,
                depositBonusAmount);

            var withdrawTransaction = TransactionRequestModelGenerator(
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

            getBalanceResponse.Balance
                .Should()
                .Be(expectedCashAmount);
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

            var balance1 = getBalanceResponse1.Balance;

            var balance2 = getBalanceResponse2.Balance;

            // Assert

            balance1.Should()
                .Be(depositCashAmount1);

            balance2
                .Should()
                .Be(depositCashAmount2);
        }

        [Fact()]
        public async Task ScenarioWithdrawAllAfterDepositRange()
        {
            var profileId1 = Guid.NewGuid().ToString();
            var profileId2 = Guid.NewGuid().ToString();

            var depositCashAmount1 = 140;
            var depositBonusAmount1 = 50;

            var depositTransaction1 = TransactionModelGenerator(
                profileId1,
                depositCashAmount1,
                depositBonusAmount1);

            var depositCashAmount2 = 100;
            var depositBonusAmount2 = 60;

            var depositTransaction2 = TransactionModelGenerator(
                profileId2,
                depositCashAmount2,
                depositBonusAmount2);

            var withdrawCashAmount1 = 140;
            var withdrawBonusAmount1 = 50;

            var withDrawModel = TransactionModelGenerator(
                profileId1,
                withdrawCashAmount1,
                withdrawBonusAmount1);

            var withdrawCashAmount2 = 100;
            var withdrawBonusAmount2 = 60;

            var withDrawModel2 = TransactionModelGenerator(
                profileId2,
                withdrawCashAmount2,
                withdrawBonusAmount2);

            var expectedCashBalance1 = depositCashAmount1 - withdrawCashAmount1;
            var expectedCashBalance2 = depositCashAmount2 - withdrawCashAmount2;


            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDrawRange");

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

            var depositRangeResponse = await scenario
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

            var withdrawRangeResponse = await scenario
                .Step($"WithdrawRange",
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

            var getBalance1Response = await scenario
                .Step($"GetBalance1",
                async () =>
                {
                    var request = new GetBalanceRequest()
                    {
                        ProfileId = profileId1
                    };
                    return await _client.GetBalanceAsync(request);
                });

            var getBalance2Response = await scenario
                .Step($"GetBalance2",
                async () =>
                {
                    var request = new GetBalanceRequest()
                    {
                        ProfileId = profileId2
                    };
                    return await _client.GetBalanceAsync(request);
                });


            getBalance2Response.Balance
                .Should()
                .Be(expectedCashBalance2);


            getBalance1Response.Balance
                .Should()
                .Be(expectedCashBalance1);
        }
    }
}