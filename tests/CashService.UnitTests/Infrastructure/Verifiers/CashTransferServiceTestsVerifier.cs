using System.Linq.Expressions;
using Moq;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models;
using CashService.BusinessLogic.Models.Criterias;

namespace CashService.UnitTests.Infrastructure.Verifiers
{
    public class CashTransferServiceTestsVerifier
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IProfileRepository> _mockProfileRepository;
        private readonly Mock<ICashProvider> _mockCashProvider;
        private readonly Mock<ITransactionProvider> _mockTransactionProvider;
        private readonly Mock<IProfileProvider> _mockProfileProvider;
        private readonly Mock<IDataContext> _mockContext;

        public ProfileEntity? ExpectedResult;
        public PagedResponse<TransactionEntity>? ExpectedResponse;
        public FilterCriteria? FilterCriteria;

        public readonly ICashService CashService;

        public CashTransferServiceTestsVerifier(
            Mock<ITransactionRepository> mockTransactionRepository,
            Mock<IProfileRepository> mockProfileRepository,
            Mock<ICashProvider> mockCashProvider,
            Mock<ITransactionProvider> mockTransactionProvider,
            Mock<IProfileProvider> mockProfileProvider,
            Mock<IDataContext> mockContext,
            ProfileEntity expectedResult,
            PagedResponse<TransactionEntity>? expectedResponse,
            FilterCriteria? filterCriteria,
            ICashService cashService)
        {
            _mockTransactionRepository = mockTransactionRepository;
            _mockProfileRepository = mockProfileRepository;
            _mockCashProvider = mockCashProvider;
            _mockTransactionProvider = mockTransactionProvider;
            _mockProfileProvider = mockProfileProvider;
            _mockContext = mockContext;
            ExpectedResult = expectedResult;
            ExpectedResponse = expectedResponse;
            FilterCriteria = filterCriteria;
            CashService = cashService;
        }

        public CashTransferServiceTestsVerifier VerifyMockCashProviderGetBalance()
        {
            _mockCashProvider
                .Verify(_ => _.GetBalance(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyMockCashProviderCalcBalanceWithinCashtype()
        {
            _mockCashProvider
                .Verify(_ => _.CalcBalanceWithinCashtype(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyMockProfileRepositoryAdd()
        {
            _mockProfileRepository
                .Verify(_ => _.Add(It.IsAny<ProfileEntity>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyMockTransactionRepositoryAddRange(Times times)
        {
            _mockTransactionRepository
                .Verify(_ => _.AddRange(It.IsAny<IEnumerable<TransactionEntity>>(), It.IsAny<CancellationToken>()), times);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyMockContextSaveChanges(Times times)
        {
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), times);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyTransactionProviderGetPagedTransactions()
        {
            _mockTransactionProvider
                .Verify(_ => _.GetPagedTransactions(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), It.IsAny<Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyTransactionProviderGetCount()
        {
            _mockTransactionProvider
                .Verify(_ => _.GetCount(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }
    }
}
