using FluentAssertions;
using Moq;
using CashService.UnitTests.Infrastructure;
using CashService.UnitTests.Infrastructure.Builders;

namespace CashService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class CashTransferServiceTests
    {
        [Fact]
        public async Task GetBalanceTest_Should_Call_GetBalance_and_Return_Result()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceExpectedResult()
                .SetupMockCashProviderGetBalance(true)
                .Build();

            //Act
            var actualResult = await verifier.CashService.GetTransactionsHistory(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            //Assert
            verifier.ExpectedResult.Should().Be(actualResult);

            verifier
                .VerifyMockCashProviderGetBalance();
        }

        [Fact]
        public async Task CalcBalanceTest_Should_Call_CalcBalance_and_Return_Result()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceExpectedResult()
                .SetupMockCashProviderCalcBalanceWithinCashtype()
                .Build();

            //Act
            var actualResult = await verifier.CashService.CalcBalanceWithinCashtype(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            //Assert
            verifier.ExpectedResult.Should().Be(actualResult);

            verifier
                .VerifyMockCashProviderCalcBalanceWithinCashtype();
        }

        [Fact]
        public async Task DepositTest_IfNotExist_Should_Call_GetBalance_Add_ProfileRepository_and_SaveChanges()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceExpectedResult()
                .SetupMockCashProviderGetBalance(false)
                .SetupMockProfileRepositoryAdd()
                .SetupMockContextSaveChanges()
                .Build();

            //Act
            await verifier.CashService.Deposit(verifier.ExpectedResult!, It.IsAny<CancellationToken>());

            //Assert
            verifier
                .VerifyMockCashProviderGetBalance()
                .VerifyMockProfileRepositoryAdd()
                .VerifyMockContextSaveChanges(Times.Once());
        }

        [Fact]
        public async Task DepositTest_IfExist_Should_Call_GetBalance_Add_TransactionRepository_and_SaveChanges()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceExpectedResult()
                .SetupMockCashProviderGetBalance(true)
                .SetupMockTransactionRepositoryAddRange()
                .SetupMockContextSaveChanges()
                .Build();

            //Act
            await verifier.CashService.Deposit(verifier.ExpectedResult!, It.IsAny<CancellationToken>());

            //Assert
            verifier
                .VerifyMockCashProviderGetBalance()
                .VerifyMockTransactionRepositoryAddRange(Times.Once())
                .VerifyMockContextSaveChanges(Times.Once());
        }

        [Fact]
        public async Task WithDrawTest_Should_Call_GetBalance_AndIfNotNull_Call_CalcBalance_Add_EntityRepository_and_SaveChanges()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceExpectedResult()
                .SetupMockCashProviderGetBalance(true)
                .SetupMockCashProviderCalcBalanceWithinCashtype()
                .SetupMockTransactionRepositoryAddRange()
                .SetupMockContextSaveChanges()
                .Build();

            //Act
            await verifier.CashService.Withdraw(verifier.ExpectedResult!, It.IsAny<CancellationToken>());

            //Assert
            verifier
                .VerifyMockCashProviderGetBalance()
                .VerifyMockCashProviderCalcBalanceWithinCashtype()
                .VerifyMockTransactionRepositoryAddRange(Times.Once())
                .VerifyMockContextSaveChanges(Times.Once());
        }

        [Fact]
        public async Task WithDrawTest_Should_Call_GetBalance_AndIfNull_Call_CalcBalance()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceExpectedResult()
                .SetupMockCashProviderGetBalance(false)
                .SetupMockCashProviderCalcBalanceWithinCashtype()
                .SetupMockTransactionRepositoryAddRange()
                .SetupMockContextSaveChanges()
                .Build();

            //Act
            await verifier.CashService.Withdraw(verifier.ExpectedResult!, It.IsAny<CancellationToken>());

            //Assert
            verifier
                .VerifyMockCashProviderGetBalance()
                .VerifyMockCashProviderCalcBalanceWithinCashtype()
                .VerifyMockTransactionRepositoryAddRange(Times.Never())
                .VerifyMockContextSaveChanges(Times.Never());
        }

        [Fact]
        public async Task GetPagedTransactionsTest()
        {
            // Arrange
            var verifier = new CashTransferServiceVerifierBuilder()
                .SetCashTransferServiceFilterCriteria()
                .SetCashTransferServicePagedResponse()
                .SetupTransactionProviderGetPagedTransactions()
                .SetupTransactionProviderGetCount()
                .Build();

            // Act
            var actualResult = await verifier.CashService.GetPagedTransactions(verifier.FilterCriteria!, It.IsAny<CancellationToken>());

            // Assert
            verifier.ExpectedResponse!.Data.Should().BeEquivalentTo(actualResult.Data);
            verifier.ExpectedResponse.TotalCount.Should().Be(actualResult.TotalCount);

            verifier
                .VerifyTransactionProviderGetPagedTransactions()
                .VerifyTransactionProviderGetCount();
        }
    }
}