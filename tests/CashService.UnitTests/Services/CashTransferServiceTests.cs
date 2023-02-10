
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.BusinessLogic.Contracts.IServices;
using CashService.BusinessLogic.Models;
using CashService.BusinessLogic.Services;
using Moq;

using static CashService.UnitTests.Support.TestTransactionProfileEntityGenerator;

namespace CashService.UnitTests.Services
{
    public class CashTransferServiceTests
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;
        private readonly Guid _guid;

        private readonly ICashService _cashService;

        private readonly Mock<IRepository<TransactionEntity>> _mockTransactionRepository;

        private readonly Mock<IRepository<TransactionProfileEntity>> _mockTransactionProfileRepository;
        private readonly Mock<ICashProvider> _mockCashProvider;

        private readonly Mock<IDataContext> _mockContext;

        public CashTransferServiceTests()
        {
            //Init moqs for IRepository IRepository IProvider IDataContext
            _mockTransactionRepository = new();

            _mockTransactionProfileRepository = new();
            _mockCashProvider = new ();

            _mockContext = new ();

            //Create Service
            _cashService = new CashTransferService(
                _mockTransactionRepository.Object,
                _mockTransactionProfileRepository.Object,
                _mockCashProvider.Object,
                _mockContext.Object);
        }

        [Fact()]
        public async Task GetBalanceTest_Should_Call_GetBalance_and_Return_Result()
        {
            // Arrange
            var expectedResult = GenerateTransactionProfile();

            //Init methods for mocks
            _mockCashProvider
                .Setup(_ => _.GetBalance(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));

            //Act
            //Call Service method
            var actualResult = await _cashService.GetBalance(new Guid(), _ctoken);

            //Assert
            //Verify method use
            _mockCashProvider
                .Verify(_ => _.GetBalance(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact()]
        public async Task CalcBalanceTest_Should_Call_CalcBalance_and_Return_Result()
        {
            // Arrange
            var expectedResult = GenerateTransactionProfile();

            //Init methods for mocks

            _mockCashProvider
                .Setup(_ => _.CalcBalance(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));


            //Act
            //Call Service method
            var actualResult = await _cashService.CalcBalance(new Guid(), _ctoken);

            //Assert
            //Verify method use
            _mockCashProvider
                .Verify(_ => _.CalcBalance(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task DepositTest_Should_Call_Add_and_SaveChanges()
        {
            // Arrange
            var expectedResult = GenerateTransactionProfile();

            //Init methods for mocks
            _mockTransactionProfileRepository
                .Setup(_ => _.Add(It.IsAny<TransactionProfileEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_=>_.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
             await _cashService.Deposit(expectedResult, _ctoken);

            //Assert
            //Verify method use
            _mockTransactionProfileRepository
                .Verify(_ => _.Add(It.IsAny<TransactionProfileEntity>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}