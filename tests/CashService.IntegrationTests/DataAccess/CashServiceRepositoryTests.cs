using CashService.BusinessLogic.Contracts.IRepositories;
using Microsoft.Extensions.DependencyInjection;
// TODO: remove unused/sort usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.EntityModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Newtonsoft.Json;

namespace CashService.IntegrationTests.DataAccess
{
    // TODO: remove empty lines
    public class CashServiceRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<TransactionProfileEntity> _transactionProfileRepository;
        // TODO: unused variable
        private readonly IRepository<TransactionEntity> _transactionRepository;

        private readonly ICashProvider _cashProvider;

        private readonly IProvider<TransactionProfileEntity> _transactionProfileProvider;
        // TODO: unused variable
        private readonly IProvider<TransactionEntity> _transactionProvider;


        private readonly IDataContext _context;
        
        public CashServiceRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _transactionProfileRepository = _scope.ServiceProvider.GetRequiredService<IRepository<TransactionProfileEntity>>();
            _transactionRepository = _scope.ServiceProvider.GetRequiredService<IRepository<TransactionEntity>>();
            _cashProvider = _scope.ServiceProvider.GetRequiredService<ICashProvider>();

            _transactionProfileProvider = _scope.ServiceProvider.GetRequiredService<IProvider<TransactionProfileEntity>>();
            _transactionProvider = _scope.ServiceProvider.GetRequiredService<IProvider<TransactionEntity>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }

        [Fact]
        public async Task AddTransactionProfile_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();

            TransactionProfileEntity expectedResult = new TransactionProfileEntity();

            // TODO: use NBuilder library for data preparation
            TransactionEntity transaction1 = new ()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                // TODO: remove comment
                //TransactionProfileEntity = expectedResult,
                CashType = CashType.Cash,
                Amount = 95,
            };
            // TODO: use NBuilder library for data preparation
            TransactionEntity transaction2 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                // TODO: remove comment
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

            // TODO: remove comment
            //string json = JsonConvert.SerializeObject(expectedResult);

            // Act
            await _transactionProfileRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _transactionProfileProvider.Get(profileId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetBalance_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();

            TransactionProfileEntity expectedResult = new TransactionProfileEntity();

            // TODO: use NBuilder library for data preparation
            TransactionEntity transaction1 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                // TODO: remove comment
                //TransactionProfileEntity = expectedResult,
                CashType = CashType.Cash,
                Amount = 95,
            };
            // TODO: use NBuilder library for data preparation
            TransactionEntity transaction2 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                // TODO: remove comment
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

            // TODO: remove comment
            //string json = JsonConvert.SerializeObject(expectedResult);

            // Act
            await _transactionProfileRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _transactionProfileProvider.Get(profileId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task CalcBalance_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();

            TransactionProfileEntity expectedResult = new TransactionProfileEntity();

            // TODO: use NBuilder library for data preparation
            TransactionEntity transaction1 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                CashType = CashType.Cash,
                Amount = 100,
            };
            // TODO: use NBuilder library for data preparation
            TransactionEntity transaction2 = new()
            {
                TransactionId = Guid.NewGuid(),
                TransactionProfileId = profileId,
                CashType = CashType.Cash,
                Amount = 50,
            };

            expectedResult.ProfileId = profileId;
            expectedResult.Transactions = new List<TransactionEntity>()
            {
                transaction1,
                transaction2
            };

            // Act
            await _transactionProfileRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _cashProvider.CalcBalance(profileId, _ctoken);

            // Assert
            actualResult.Transactions[0].Amount.Should()
                .Be(150);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
