using System.Linq.Expressions;
using FizzWare.NBuilder;
using Moq;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models;
using CashService.BusinessLogic.Models.Enums;
using CashService.BusinessLogic.Services;
using CashService.UnitTests.Infrastructure.Verifiers;
using CashService.BusinessLogic.Models.Criterias;
using CashService.DataAccess;

namespace CashService.UnitTests.Infrastructure.Builders
{
    public class CashTransferServiceVerifierBuilder
    {
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<IProfileRepository> _mockProfileRepository;
        private readonly Mock<ICashProvider> _mockCashProvider;
        private readonly Mock<ITransactionProvider> _mockTransactionProvider;
        private readonly Mock<IProfileProvider> _mockProfileProvider;
        private readonly Mock<IDataContext> _mockContext;
        private readonly IResilientService _resilientService;

        private ProfileEntity? _expectedResult;
        private PagedResponse<TransactionEntity>? _expectedResponse;
        private FilterCriteria? _filterCriteria;

        private readonly ICashService _cashService;

        public CashTransferServiceVerifierBuilder()
        {
            //Init moqs for IRepository IRepository IProvider IDataContext
            _mockTransactionRepository = new();
            _mockProfileRepository = new();

            _mockCashProvider = new();

            _mockTransactionProvider = new();
            _mockProfileProvider = new();

            _mockContext = new();

            _resilientService = new ResilientService(_mockContext.Object);

            //Create Service
            _cashService = new CashTransferService(
                _mockTransactionRepository.Object,
                _mockProfileRepository.Object,
                _mockCashProvider.Object,
                _mockTransactionProvider.Object,
                _mockProfileProvider.Object,
                _mockContext.Object,
                _resilientService);
        }

        public CashTransferServiceVerifierBuilder SetCashTransferServiceExpectedResult()
        {
            var transactions = Builder<TransactionEntity>
                .CreateListOfSize(2)
                .All()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId = Guid.NewGuid())
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = 50)
                .Build();

            _expectedResult = Builder<ProfileEntity>
                .CreateNew()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.Transactions = transactions.ToList())
                .Build();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetCashTransferServicePagedResponse()
        {
            var transactions = Builder<TransactionEntity>
                .CreateListOfSize(2)
                .All()
                .With(x => x.Id = Guid.NewGuid())
                .With(x => x.ProfileId = Guid.NewGuid())
                .With(x => x.CashType = CashType.Bonus)
                .And(x => x.Amount = 50)
                .Build();

            _expectedResponse = Builder<PagedResponse<TransactionEntity>>
                .CreateNew()
                .With(x => x.Data = transactions.ToList())
                .Build();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetCashTransferServiceFilterCriteria()
        {
            _filterCriteria = Builder<FilterCriteria>
                .CreateNew()
                .Build();

            return this;
        }

        public CashTransferServiceTestsVerifier Build()
        {
            return new CashTransferServiceTestsVerifier(
                _mockTransactionRepository,
                _mockProfileRepository,
                _mockCashProvider,
                _mockTransactionProvider,
                _mockProfileProvider,
                _mockContext,
                _expectedResult,
                _expectedResponse,
                _filterCriteria,
                _cashService,
                _resilientService);
        }

        public CashTransferServiceVerifierBuilder SetupMockCashProviderGetBalance(bool isExpectedResultExists)
        {
            _mockCashProvider
                .Setup(_ => _.GetTransactionsHistory(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
                .ReturnsAsync(isExpectedResultExists ? _expectedResult! : default(ProfileEntity))
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupMockCashProviderCalcBalanceWithinCashtype()
        {
            _mockCashProvider
                .Setup(_ => _.CalcBalanceWithinCashtype(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResult!)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupMockProfileRepositoryAdd()
        {
            _mockProfileRepository
                .Setup(_ => _.Add(It.IsAny<ProfileEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupMockProfileRepositoryUpdate()
        {
            _mockProfileRepository
                .Setup(_ => _.Update(It.IsAny<ProfileEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupProfileProviderGet()
        {
            _mockProfileProvider
                .Setup(_ => _.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
                .ReturnsAsync(_expectedResult)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupMockTransactionRepositoryAddRange()
        {
            _mockTransactionRepository
                .Setup(_ => _.AddRange(It.IsAny<IEnumerable<TransactionEntity>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupMockContextSaveChanges()
        {
            _mockContext
                .Setup(_ => _.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupTransactionProviderGetPagedTransactions()
        {
            _mockTransactionProvider
                .Setup(_ => _.GetPagedTransactions(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), It.IsAny<Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResponse!.Data)
                .Verifiable();

            return this;
        }

        public CashTransferServiceVerifierBuilder SetupTransactionProviderGetCount()
        {
            _mockTransactionProvider
                .Setup(_ => _.GetCount(It.IsAny<Expression<Func<TransactionEntity, bool>>>(), It.IsAny<CancellationToken>()))!
                .ReturnsAsync(_expectedResponse!.TotalCount)
                .Verifiable();

            return this;
        }
    }
}
