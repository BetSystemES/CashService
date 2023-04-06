using Microsoft.Extensions.DependencyInjection;
using CashService.BusinessLogic.Contracts.Providers;
using FluentAssertions;
using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Services;
using CashService.DataAccess;
using CashService.DataAccess.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        private readonly ITransactionProvider _transactionProvider;

        private readonly IProfileService _profileService;
        private readonly ICashService _cashService;
        private readonly IResilientService _resilientService;

        private readonly IDataContext _context;

        private readonly IConfiguration _configuration;

        public CashServiceRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _profileRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _transactionRepository = _scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
            _cashProvider = _scope.ServiceProvider.GetRequiredService<ICashProvider>();
            _transactionProvider = _scope.ServiceProvider.GetRequiredService<ITransactionProvider>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();

            _resilientService = _scope.ServiceProvider.GetRequiredService<IResilientService>();
            _profileService = _scope.ServiceProvider.GetRequiredService<IProfileService>();
            _cashService = _scope.ServiceProvider.GetRequiredService<ICashService>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        [Fact]
        public async Task Test_Transactions_Deposit()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileEntity profileEntity = GenerateCashProfileEntity(profileId, 50);
            await AddProfileEntity(profileEntity, _ctoken);

            var expectedResult = 150;

            // Act
            var tryCounts = Enumerable.Range(0, 10);
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 5
            };

            await Parallel.ForEachAsync(tryCounts, parallelOptions, async (tryCount, token) =>
            {
                var depositProfileEntity = GenerateCashProfileEntity(profileId, 10);
                var serviceBuilder = PrepareCashTransferService();
                var cashTransferService = serviceBuilder.GetRequiredService<ICashService>();
                await cashTransferService.Deposit(depositProfileEntity, token);
            });

            // Assert
            _profileRepository.Detach(profileEntity);

            var actualResult = await _profileService.Get(profileId, _ctoken);

            actualResult.CashAmount.Should().Be(expectedResult);
        }

        [Fact]
        public async Task Test_Transactions_Widthdraw()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileEntity profileEntity = GenerateCashProfileEntity(profileId, 95);
            await AddProfileEntity(profileEntity, _ctoken);

            var expectedResult = 5;

            // Act
            var tryCounts = Enumerable.Range(0, 10);
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 5
            };

            await Parallel.ForEachAsync(tryCounts, parallelOptions, async (tryCount, token) =>
            {
                var depositProfileEntity = GenerateCashProfileEntity(profileId, -10);
                var serviceBuilder = PrepareCashTransferService();
                var cashTransferService = serviceBuilder.GetRequiredService<ICashService>();
                await cashTransferService.Withdraw(depositProfileEntity, token);
            });

            // Assert
            _profileRepository.Detach(profileEntity);

            var actualResult = await _profileService.Get(profileId, _ctoken);

            actualResult.Should().Be(actualResult);
        }

        [Fact]
        public async Task AddTransactionProfile_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileEntity expectedResult = GenerateEmptyProfileEntity(profileId);

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

            var initProfileEntity = GenerateEmptyProfileEntity(profileId);
            await _profileService.Create(initProfileEntity.Id, _ctoken);

            var depositProfileEntity = GenerateCashProfileEntity(profileId, 100, 50);
            await _cashService.Deposit(depositProfileEntity, _ctoken);

            //Act
            var actualResult = await _cashService.GetBalance(profileId, _ctoken);
            var result = await _profileService.Get(profileId, _ctoken);

            //Assert
            actualResult.Should().Be(150);
            result.CashAmount.Should().Be(150);
        }

        [Fact]
        public async Task CalcBalanceWithinCashtype_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();

            var initProfileEntity = GenerateEmptyProfileEntity(profileId);
            await _profileService.Create(initProfileEntity.Id, _ctoken);

            ProfileEntity depositCashProfileEntity = GenerateCashProfileEntity(profileId, 100, 50);
            await _cashService.Deposit(depositCashProfileEntity, _ctoken);

            ProfileEntity depositBonusProfileEntity = GenerateBonusProfileEntity(profileId, 30, 20);
            await _cashService.Deposit(depositBonusProfileEntity, _ctoken);

            // Act
            var actualResult = await _cashProvider.CalcBalanceWithinCashtype(profileId, _ctoken);

            // Assert
            actualResult.Transactions[0].Amount.Should()
                .Be(150);

            actualResult.Transactions[1].Amount.Should()
                .Be(50);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        private async Task AddProfileEntity(ProfileEntity entity, CancellationToken cancellationToken)
        {
            await _profileRepository.Add(entity, cancellationToken);
            await _context.SaveChanges(cancellationToken);
        }

        private IServiceProvider PrepareCashTransferService()
        {
            var services = new ServiceCollection();

            services.AddScoped<ICashService, CashTransferService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IResilientService, ResilientService>();
            services.AddScoped<IDataContext, CashDataContext>();

            services.AddProviders().AddRepositories().AddPostgreSqlContext(options => { options.UseNpgsql(_configuration.GetConnectionString("CashDb"), opt => opt.EnableRetryOnFailure(3)); });

            services.AddLogging(logging => logging.AddConsole());

            return services.BuildServiceProvider();
        }
    }
}