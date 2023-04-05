using Microsoft.Extensions.DependencyInjection;
using CashService.BusinessLogic.Contracts.Providers;
using FluentAssertions;
using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Services;
using static CashService.IntegrationTests.DataAccess.DataGenerator;
using CashService.BusinessLogic.Contracts.Services;

namespace CashService.IntegrationTests.DataAccess
{
    public class CashServiceRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly ITransactionRepository _transactionRepository;

        private readonly ICashProvider _cashProvider;
        private readonly IProfileService _profileService;

        private readonly ITransactionProvider _transactionProvider;

        private readonly IDataContext _context;

        private readonly IResilientService _resilientService;
        
        public CashServiceRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _transactionRepository = _scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
            _cashProvider = _scope.ServiceProvider.GetRequiredService<ICashProvider>();
            _profileService = _scope.ServiceProvider.GetRequiredService<IProfileService>();

            _transactionProvider = _scope.ServiceProvider.GetRequiredService<ITransactionProvider>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();

            _resilientService = _scope.ServiceProvider.GetRequiredService<IResilientService>();
        }

        [Fact]
        public async Task Test_Transactions_Deposit()
        {
            // Arrange
            var cashTransferService = new CashTransferService(
                _transactionRepository,
                _cashProvider,
                _transactionProvider,
                _profileService,
                _context,
                _resilientService);

            var cashTransferService2 = new CashTransferService(
                _transactionRepository,
                _cashProvider,
                _transactionProvider,
                _profileService,
                _context,
                _resilientService);

            var profileId = Guid.NewGuid();
            ProfileEntity profileEntity = GenerateProfileEntity(profileId, 50, 0);
            ProfileEntity profileEntity2 = GenerateProfileEntity(profileId, 40, 0);

            // Act
            await cashTransferService.Deposit(profileEntity, _ctoken);
            await cashTransferService2.Deposit(profileEntity2, _ctoken);

            var actualResult = await _profileService.Get(profileId, _ctoken);
        }

        [Fact]
        public async Task Test_Transactions_Widthdraw()
        {
            // Arrange
            var cashTransferService = new CashTransferService(
                _transactionRepository,
                _cashProvider,
                _transactionProvider,
                _profileService,
                _context,
                _resilientService);

            var cashTransferService2 = new CashTransferService(
                _transactionRepository,
                _cashProvider,
                _transactionProvider,
                _profileService,
                _context,
                _resilientService);

            var profileId = Guid.NewGuid();
            var profileEntity = GenerateProfileEntity(profileId, 95, 50);
            var profileEntity2 = GenerateProfileEntity(profileId, 50, 0);
            var profileEntity3 = GenerateProfileEntity(profileId, 40, 00);

            // Act
            await cashTransferService.Deposit(profileEntity, _ctoken);

            await cashTransferService.Withdraw(profileEntity2, _ctoken);
            await cashTransferService2.Withdraw(profileEntity3, _ctoken);

            var actualResult = await _profileService.Get(profileId, _ctoken);
        }

        [Fact]
        public async Task AddTransactionProfile_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileEntity expectedResult = GenerateProfileEntity(profileId, 95,50);

            // Act
            await _profileService.Create(expectedResult.Id, _ctoken);

            var actualResult = await _profileService.Get(profileId, _ctoken);

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
            await _profileService.Create(expectedResult.Id, _ctoken);

            var actualResult = await _profileService.Get(profileId, _ctoken);

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
            await _profileService.Create(expectedResult.Id, _ctoken);

            var actualResult = await _cashProvider.CalcBalanceWithinCashtype(profileId, _ctoken);

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
