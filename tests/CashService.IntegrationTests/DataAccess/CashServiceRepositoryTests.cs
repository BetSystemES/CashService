﻿using Microsoft.Extensions.DependencyInjection;
using CashService.BusinessLogic.Contracts.Providers;
using FluentAssertions;
using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using static CashService.IntegrationTests.DataAccess.DataGenerator;

namespace CashService.IntegrationTests.DataAccess
{
    public class CashServiceRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IProfileRepository _profileRepository;
        private readonly ITransactionRepository _transactionRepository;

        private readonly ICashProvider _cashProvider;

        private readonly IProfileProvider _profileProvider;
        private readonly ITransactionProvider _transactionProvider;

        private readonly IDataContext _context;
        
        public CashServiceRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _profileRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _transactionRepository = _scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
            _cashProvider = _scope.ServiceProvider.GetRequiredService<ICashProvider>();

            _profileProvider = _scope.ServiceProvider.GetRequiredService<IProfileProvider>();
            _transactionProvider = _scope.ServiceProvider.GetRequiredService<ITransactionProvider>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }

        [Fact]
        public async Task AddTransactionProfile_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileEntity expectedResult = GenerateProfileEntity(profileId, 95,50);

            // Act
            await _profileRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _profileProvider.Get(profileId, _ctoken);

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
            ProfileEntity expectedResult = GenerateProfileEntity(profileId, 95, 50);

            // Act
            await _profileRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _profileProvider.Get(profileId, _ctoken);

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
            ProfileEntity expectedResult = GenerateCashProfileEntity(profileId, 100, 50);

            // Act
            await _profileRepository.Add(expectedResult, _ctoken);
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
