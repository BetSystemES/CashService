using System.Collections;
using CashService.FunctionalTests;
using CashService.FunctionalTests.Adapters;
using Xunit.Abstractions;

using FluentAssertions;
using NScenario;
using CashService.BusinessLogic;
using CashService.BusinessLogic.Models;
using CashService.GRPC;
using Newtonsoft.Json;
using static CashService.GRPC.Casher;
using CashType = CashService.GRPC.CashType;
using System.Security.Policy;
using static CashService.FunctionalTests.Scenaries.DataGenerator;


namespace CashService.FunctionalTests.Scenaries
{
    public class ScenarioCashServiceTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly CasherClient _client;

        public ScenarioCashServiceTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new CasherClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioGetBalance()
        {
            var profileId = Guid.NewGuid().ToString();
            var transactionModel = TransactionModelGenerator(profileId, 95,50);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"GetBalance");

            var addDepositResponse = await scenario
                .Step($"Deposit",
                async () =>
                {
                    var request = new DepositRequest()
                    {
                        Deposit = transactionModel,
                    };

                    string json = JsonConvert.SerializeObject(request);

                    return await _client.DepositAsync(request);
                });

            var getBalanceResponce = await scenario
                .Step($"GetBalance",
                async () =>
                {
                    var request = new GetBalanceRequest()
                    { 
                        Profileid = profileId
                    };
                    return await _client.GetBalanceAsync(request);
                });

            var result = getBalanceResponce.Balance;

            result
                .Should()
                .NotBeNull()
                .Equals(transactionModel);
        }


        [Fact()]
        public async Task ScenarioWithDraw0()
        {
            var profileId = Guid.NewGuid().ToString();

            var withDrawModel = TransactionModelGenerator(profileId, 100, 60);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDraw0");

            var withdrawResponce = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withDrawModel,
                        };

                        string json = JsonConvert.SerializeObject(request);

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponce.Withdrawresponce;

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

            var transactionModel = TransactionModelGenerator(profileId, 140, 50);

            var withDrawModel = TransactionModelGenerator(profileId, 100, 60);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"WithDraw");

            var addDepositResponse = await scenario
                .Step($"Deposit",
                    async () =>
                    {
                        var request = new DepositRequest()
                        {
                            Deposit = transactionModel,
                        };

                        string json = JsonConvert.SerializeObject(request);

                        return await _client.DepositAsync(request);
                    });

            var withdrawResponce = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withDrawModel,
                        };

                        string json = JsonConvert.SerializeObject(request);

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponce.Withdrawresponce;

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

            var addDepositResponse = await scenario
                .Step($"Deposit",
                    async () =>
                    {
                        var request = new DepositRequest()
                        {
                            Deposit = transactionModel,
                        };

                        string json = JsonConvert.SerializeObject(request);

                        return await _client.DepositAsync(request);
                    });

            var withdrawResponce = await scenario
                .Step($"WithDraw",
                    async () =>
                    {
                        var request = new WithdrawRequest()
                        {
                            Withdrawrequest = withDrawModel,
                        };

                        string json = JsonConvert.SerializeObject(request);

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponce.Withdrawresponce;
            //int result = 50;

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
            var profileId = Guid.NewGuid().ToString();
            var transactionModel = TransactionModelGenerator(profileId, 140, 50);

            var profileId2 = Guid.NewGuid().ToString();
            var transactionModel2 = TransactionModelGenerator(profileId2, 100, 60);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"DepositRange");

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

                        request.Depositrangerequest.AddRange(transactionModels);

                        string json = JsonConvert.SerializeObject(request).ToLower();

                        return await _client.DepositRangeAsync(request);
                    });

            var getBalanceResponce = await scenario
                .Step($"GetBalance",
                    async () =>
                    {
                        var request = new GetBalanceRequest()
                        {
                            Profileid = profileId
                        };
                        return await _client.GetBalanceAsync(request);
                    });

            var getBalanceResponce2 = await scenario
                .Step($"GetBalance2",
                    async () =>
                    {
                        var request = new GetBalanceRequest()
                        {
                            Profileid = profileId2
                        };
                        return await _client.GetBalanceAsync(request);
                    });

            var result = getBalanceResponce.Balance;

            var result2 = getBalanceResponce2.Balance;

            result.Transactions.Count
                .Should()
                .Be(2);

            result
                .Should()
                .NotBeNull()
                .Equals(transactionModel);

            result2.Transactions.Count
                .Should()
                .Be(2);

            result2
                .Should()
                .NotBeNull()
                .Equals(transactionModel2);
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

                        request.Depositrangerequest.AddRange(transactionModels);

                        string json = JsonConvert.SerializeObject(request).ToLower();

                        return await _client.DepositRangeAsync(request);
                    });

            var withdrawRangeResponce = await scenario
                .Step($"WithDrawRange",
                    async () =>
                    {
                        var request = new WithdrawRangeRequest()
                        {
                           Withdrawrangerequest =
                           {
                               withDrawModel,
                               withDrawModel2
                           }
                        };

                        string json = JsonConvert.SerializeObject(request).ToLower();

                        return await _client.WithdrawRangeAsync(request);
                    });

         
            var result = withdrawRangeResponce.Withdrawrangeresponce[0];
            var result2 = withdrawRangeResponce.Withdrawrangeresponce[1];

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
