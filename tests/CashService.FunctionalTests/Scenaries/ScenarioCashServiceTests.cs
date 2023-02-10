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

            TransactionModel transactionModel = new ();

            Transaction transaction1 = new()
            {
               Transactionid = Guid.NewGuid().ToString(),
               Cashtype = CashType.Cash,
                Amount = 95,
            };
            Transaction transaction2 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Bonus,
                Amount = 50,
            };

            transactionModel.Profileid = profileId;
            transactionModel.Transactions.Add(transaction1);
            transactionModel.Transactions.Add(transaction2);


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
        public async Task ScenarioWithDraw()
        {
            var profileId = Guid.NewGuid().ToString();

            TransactionModel transactionModel = new();

            Transaction transaction1 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Cash,
                Amount = 140,
            };
            Transaction transaction2 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Bonus,
                Amount = 50,
            };

            transactionModel.Profileid = profileId;
            transactionModel.Transactions.Add(transaction1);
            transactionModel.Transactions.Add(transaction2);

            TransactionModel withDrawModel = new();

            Transaction transaction3 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Cash,
                Amount = 100,
            };
            Transaction transaction4 = new()
            {
                Transactionid = Guid.NewGuid().ToString(),
                Cashtype = CashType.Bonus,
                Amount = 60,
            };

            withDrawModel.Profileid = profileId;
            withDrawModel.Transactions.Add(transaction3);
            withDrawModel.Transactions.Add(transaction4);


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
                            Withdrawrequest = transactionModel,
                        };

                        string json = JsonConvert.SerializeObject(request);

                        return await _client.WithdrawAsync(request);
                    });

            var result = withdrawResponce.Withdrawresponce;

            result.Transactions[0].Amount
                .Should()
                .Equals(100);

            result.Transactions[1].Amount
                .Should()
                .Equals(50);
        }
    }
}
