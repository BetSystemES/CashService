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

        //[Fact]
        //public async Task DepositTest()
        //{
        //    // Arrange
        //    var verifier = new CashTransferServiceVerifierBuilder()
        //        .SetCashTransferServiceExpectedResult()
        //        .SetupProfileProviderGet()
        //        .SetupMockProfileRepositoryUpdate()
        //        .SetupMockTransactionRepositoryAddRange()
        //        .SetupMockContextSaveChanges()
        //        .Build();

        //    //Act
        //    var result = await verifier.CashService.Deposit(verifier.ExpectedResult!, It.IsAny<CancellationToken>());

        //    //Assert
        //    verifier.ExpectedResult.Should().Be(result);

        //    verifier
        //        .VerifyProfileProviderGet()
        //        .VerifyMockProfileRepositoryUpdate()
        //        .VerifyMockTransactionRepositoryAddRange(Times.Once())
        //        .VerifyMockContextSaveChanges(Times.Once());
        //}

        //[Fact]
        //public async Task WithdrawTest()
        //{
        //    // Arrange
        //    var verifier = new CashTransferServiceVerifierBuilder()
        //        .SetCashTransferServiceExpectedResult()
        //        .SetupProfileProviderGet()
        //        .SetupMockProfileRepositoryUpdate()
        //        .SetupMockTransactionRepositoryAddRange()
        //        .SetupMockContextSaveChanges()
        //        .Build();

        //    //Act
        //    await verifier.CashService.Withdraw(verifier.ExpectedResult!, It.IsAny<CancellationToken>());

        //    //Assert
        //    verifier
        //        .VerifyProfileProviderGet()
        //        .VerifyMockProfileRepositoryUpdate()
        //        .VerifyMockTransactionRepositoryAddRange(Times.Once())
        //        .VerifyMockContextSaveChanges(Times.Once());
        //}

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