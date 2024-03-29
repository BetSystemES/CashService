﻿using System.Linq.Expressions;
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
        private readonly Mock<IProfileService> _mockProfileService;
        private readonly Mock<ICashProvider> _mockCashProvider;
        private readonly Mock<ITransactionProvider> _mockTransactionProvider;
        private readonly Mock<IDataContext> _mockContext;
        private readonly IResilientService _resilientService;

        public ProfileEntity? ExpectedResult;
        public PagedResponse<TransactionEntity>? ExpectedResponse;
        public FilterCriteria? FilterCriteria;

        public readonly ICashService CashService;

        public CashTransferServiceTestsVerifier(
            Mock<ITransactionRepository> mockTransactionRepository,
            Mock<IProfileService> mockProfileService,
            Mock<ICashProvider> mockCashProvider,
            Mock<ITransactionProvider> mockTransactionProvider,
            Mock<IDataContext> mockContext,
            ProfileEntity expectedResult,
            PagedResponse<TransactionEntity>? expectedResponse,
            FilterCriteria? filterCriteria,
            ICashService cashService,
            IResilientService resilientService)
        {
            _mockTransactionRepository = mockTransactionRepository;
            _mockProfileService = mockProfileService;
            _mockCashProvider = mockCashProvider;
            _mockTransactionProvider = mockTransactionProvider;
            _mockContext = mockContext;
            ExpectedResult = expectedResult;
            ExpectedResponse = expectedResponse;
            FilterCriteria = filterCriteria;
            CashService = cashService;
            _resilientService = resilientService;
        }

        public CashTransferServiceTestsVerifier VerifyMockCashProviderGetBalance()
        {
            _mockCashProvider
                .Verify(_ => _.GetTransactionsHistory(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

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
            _mockProfileService
                .Verify(_ => _.Create(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyMockProfileRepositoryUpdate()
        {
            _mockProfileService
                .Verify(_ => _.Update(It.IsAny<ProfileEntity>(), It.IsAny<CancellationToken>()));

            return this;
        }

        public CashTransferServiceTestsVerifier VerifyProfileProviderGet()
        {
            _mockProfileService
                .Verify(_ => _.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

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
