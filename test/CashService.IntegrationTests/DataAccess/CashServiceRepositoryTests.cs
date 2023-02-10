using CashService.BusinessLogic.Contracts.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Newtonsoft.Json;

namespace CashService.IntegrationTests.DataAccess
{
    public class CashServiceRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<TransactionProfileEntity> _transactionProfileRepository;
        private readonly IRepository<TransactionEntity> _transactionRepository;
        private readonly ICashProvider _cashProvider;

        private readonly IDataContext _context;
        public CashServiceRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _transactionProfileRepository = _scope.ServiceProvider.GetRequiredService<IRepository<TransactionProfileEntity>>();
            _transactionRepository = _scope.ServiceProvider.GetRequiredService<IRepository<TransactionEntity>>();
            _cashProvider = _scope.ServiceProvider.GetRequiredService<ICashProvider>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }

        [Fact]
        public async Task AddTransactionProfile_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();

            TransactionProfileEntity expectedResult = new TransactionProfileEntity();

            TransactionEntity transaction1 = new ()
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

            //string json = JsonConvert.SerializeObject(expectedResult);

            // Act
            await _transactionProfileRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _transactionProfileRepository.Get(profileId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }
        
        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
