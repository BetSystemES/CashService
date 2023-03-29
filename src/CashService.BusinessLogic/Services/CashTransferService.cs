using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Extensions;
using CashService.BusinessLogic.Models;
using CashService.BusinessLogic.Models.Criterias;
using CashService.BusinessLogic.Models.Enums;
using System.Linq.Expressions;
using CashService.BusinessLogic.Helpers;

namespace CashService.BusinessLogic.Services
{
    public class CashTransferService : ICashService
    {
        private readonly ITransactionRepository _transactionEntityRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ICashProvider _cashProvider;

        private readonly ITransactionProvider _transactionEntityProvider;
        private readonly IProfileProvider _profileProvider;

        private readonly IDataContext _context;

        public CashTransferService(
            ITransactionRepository transactionEntityRepository,
            IProfileRepository profileRepository,
            ICashProvider cashProvider,
            ITransactionProvider transactionEntityProvider,
            IProfileProvider profileProvider,
            IDataContext context)
        {
            _transactionEntityRepository=transactionEntityRepository;
            _profileRepository=profileRepository;
            _cashProvider=cashProvider;

            _transactionEntityProvider = transactionEntityProvider;
            _profileProvider = profileProvider;

            _context=context;
        }

        public async Task<ProfileEntity> GetBalance(Guid profileId, CancellationToken token)
        {
            ProfileEntity balance = await _cashProvider.GetBalance(profileId, token);
            return balance;
        }

        public async Task<PagedResponse<TransactionEntity>> GetPagedTransactions(FilterCriteria filterCriteria, CancellationToken cancellationToken)
        {
            //var expressionProfile = GetFilterExpressionByProfileEntity(filterCriteria);
            var expressionTransaction = GetFilterExpressionByTransactionEntity(filterCriteria);
            var orderTransaction = GetOrderByFunc(filterCriteria);

            var (skipTransaction, takeTransaction) = ((PaginationCriteria)filterCriteria).GetPaginationCriteria();

            var pagedTransactions = await _transactionEntityProvider.GetPagedTransactions(
                expressionTransaction,
                orderTransaction,
                skipTransaction, takeTransaction, 
                cancellationToken);

            var totalCount = await _transactionEntityProvider.GetCount(
                expressionTransaction,
                cancellationToken);

            var pagedResponse = new PagedResponse<TransactionEntity>()
            {
                TotalCount = totalCount,
                Data = pagedTransactions
            };

            return pagedResponse;
        }

        public async Task<ProfileEntity> CalcBalanceWithinCashtype(Guid profileId, CancellationToken token)
        {
            ProfileEntity balance = await _cashProvider.CalcBalanceWithinCashtype(profileId, token);
            return balance;
        }

        public async Task Deposit(ProfileEntity depositProfile, CancellationToken token)
        {
            ProfileEntity balance = await _cashProvider.GetBalance(depositProfile.Id, token);

            if (balance is null)
            {
                await _profileRepository.Add(depositProfile, token);
            }
            else
            {
                await _transactionEntityRepository.AddRange(depositProfile.Transactions, token);
            }
            await _context.SaveChanges(token);
        }

        public async Task<ProfileEntity> Withdraw(ProfileEntity withdrawProfile, CancellationToken token)
        {
            var profileId = withdrawProfile.Id;

            ProfileEntity balance = await _cashProvider.GetBalance(profileId, token);

            if (balance is not null)
            {
                balance = await _cashProvider.CalcBalanceWithinCashtype(profileId, token);

                withdrawProfile.CheckForUnite();

                balance.CalsIntersectionByCashType(withdrawProfile);

                var differenceList = withdrawProfile.DifferenceTransaction(balance);

                withdrawProfile.ReCalcBalanceAndWithDraw(differenceList, balance);

                await _transactionEntityRepository.AddRange(withdrawProfile.Transactions, token);
                await _context.SaveChanges(token);
            }
            else
            {
                balance = await _cashProvider.CalcBalanceWithinCashtype(profileId, token);
            }

            return balance;
        }

        public async Task DepositRange(List<ProfileEntity> depositRangeProfileEntities, CancellationToken token)
        {
            foreach (var profileEntity in depositRangeProfileEntities)
            {
               await Deposit(profileEntity, token);
            }
        }

        public async Task<List<ProfileEntity>> WithdrawRange(List<ProfileEntity> withdrawRangeProfileEntities, CancellationToken token)
        {
            List<ProfileEntity> transactionProfileEntities = new();
            foreach (var profileEntity in withdrawRangeProfileEntities)
            {
               var result = await Withdraw(profileEntity, token);
               transactionProfileEntities.Add(result);
            }
            return transactionProfileEntities;
        }

        private Expression<Func<ProfileEntity, bool>> GetFilterExpressionByProfileEntity(FilterCriteria filter)
        {
            var predicate = PredicateBuilderHelper.True<ProfileEntity>();

            if (filter.UserIds != null && filter.UserIds.Any())
            {
                predicate = predicate.And(x => filter.UserIds.Contains(x.Id));
            }

            return predicate;
        }

        private Expression<Func<TransactionEntity, bool>> GetFilterExpressionByTransactionEntity(FilterCriteria filter)
        {
            var predicate = PredicateBuilderHelper.True<TransactionEntity>();

            if (filter.UserIds != null && filter.UserIds.Any())
            {
                predicate = predicate.And(x => filter.UserIds.Contains(x.ProfileId));
            }

            if (filter.CashType.HasValue)
            {
                predicate = predicate.And(x => x.CashType == filter.CashType.Value);
            }

            predicate = predicate.FilterPredicateByAmount(filter);

            predicate = predicate.FilterPredicateByDate(filter);

            return predicate;
        }

        private Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>> GetOrderByFunc(FilterCriteria filter)
        {
            Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>> orderByExpression = null;

            if (!string.IsNullOrEmpty(filter.ColumnName) && filter.OrderDirection.HasValue && filter.OrderDirection != OrderDirection.Unspecified)
            {
                orderByExpression = OrderHelper.GetOrderBy<TransactionEntity>(filter.ColumnName, filter.OrderDirection.Value);
            }

            return orderByExpression;
        }
    }
}