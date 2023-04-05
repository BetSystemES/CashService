using System.Data;
using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Extensions;
using CashService.BusinessLogic.Models.Criterias;
using CashService.BusinessLogic.Models.Enums;
using System.Linq.Expressions;
using CashService.BusinessLogic.Helpers;
using CashService.BusinessLogic.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Polly;

namespace CashService.BusinessLogic.Services
{
    public class CashTransferService : ICashService
    {
        private readonly ITransactionRepository _transactionEntityRepository;
        private readonly ICashProvider _cashProvider;

        private readonly ITransactionProvider _transactionEntityProvider;

        private readonly IDataContext _context;

        private readonly IResilientService _resilientService;
        private readonly IProfileService _profileService;

        public CashTransferService(
            ITransactionRepository transactionEntityRepository,
            ICashProvider cashProvider,
            ITransactionProvider transactionEntityProvider,
            IDataContext context,
            IResilientService resilientService,
            IProfileService profileService)
        {
            _transactionEntityRepository = transactionEntityRepository;
            _cashProvider = cashProvider;

            _transactionEntityProvider = transactionEntityProvider;

            _context = context;

            _resilientService = resilientService;
            _profileService = profileService;
        }

        public async Task<ProfileEntity> GetTransactionsHistory(Guid profileId, CancellationToken token)
        {
            ProfileEntity balance = await _cashProvider.GetTransactionsHistory(profileId, token);
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

        public async Task<decimal> GetBalance(Guid profileId, CancellationToken token)
        {
            var profile = await _profileService.Get(profileId, token);
            return profile.CashAmount;
        }

        public async Task<ProfileEntity> Deposit(ProfileEntity depositProfile, CancellationToken token)
        {
            ProfileEntity? profile = default;
            EntityEntry? entry = default;

            var policyResult = await Policy
                .Handle<DBConcurrencyException>()
                .Or<DbUpdateConcurrencyException>()
                .WaitAndRetryAsync(3, retryAttempt =>
                        TimeSpan.FromMilliseconds(Math.Pow(5, retryAttempt)),
                    (_, _) =>
                    {
                        Console.WriteLine("WaitAndRetryAsync");

                        if (profile is not null)
                        {
                            _profileService.Detach(profile);
                            entry = _profileService.Entry(profile);
                        }
                    }
                )
                .ExecuteAndCaptureAsync(async retryToken =>
                {
                    await _resilientService.ExecuteAsync(async () =>
                        {
                            if (profile is null || entry?.State == EntityState.Detached)
                                profile = await _profileService.Get(depositProfile.Id, retryToken);

                            if (profile is null)
                                throw new Exception("entity was not found for id");

                            profile.CashAmount += depositProfile.Transactions
                                .Where(x => x.CashType == CashType.Cash)
                                .Sum(x => x.Amount);

                            await _profileService.Update(profile, retryToken);

                            await _transactionEntityRepository.AddRange(depositProfile.Transactions, retryToken);

                            await _context.SaveChanges(retryToken);

                            depositProfile.CashAmount = profile.CashAmount;
                        },
                        IsolationLevel.ReadCommitted, retryToken);
                }, token);

            if (policyResult.FinalException is not null)
            {
                Console.WriteLine(policyResult.FinalException.Message);
                // throw policyResult.FinalException;
            }

            return depositProfile;
        }

        public async Task<ProfileEntity> Withdraw(ProfileEntity withdrawProfile, CancellationToken token)
        {
            ProfileEntity? profile = default;
            EntityEntry? entry = default;

            var policyResult = await Policy
                .Handle<DBConcurrencyException>()
                .Or<DbUpdateConcurrencyException>()
                .WaitAndRetryAsync(3, retryAttempt =>
                        TimeSpan.FromMilliseconds(Math.Pow(5, retryAttempt)),
                    (_, _) =>
                    {
                        Console.WriteLine("WaitAndRetryAsync");

                        if (profile is not null)
                        {
                            _profileService.Detach(profile);
                            entry = _profileService.Entry(profile);
                        }
                    }
                )
                .ExecuteAndCaptureAsync(async retryToken =>
                {
                    await _resilientService.ExecuteAsync(async () =>
                    {
                        if (profile is null || entry?.State == EntityState.Detached)
                            profile = await _profileService.Get(withdrawProfile.Id, retryToken);

                        if (profile is null)
                            throw new Exception("entity was not found for id");

                        profile.CashAmount -= withdrawProfile.Transactions
                            .Where(x => x.CashType == CashType.Cash)
                            .Sum(x => x.Amount);

                        if (profile.CashAmount < 0)
                        {
                            throw new Exception("not enough money");
                        }

                        await _profileService.Update(profile, retryToken);

                        await _transactionEntityRepository.AddRange(withdrawProfile.Transactions, retryToken);

                        await _context.SaveChanges(retryToken);

                        withdrawProfile.CashAmount = profile.CashAmount;
                    },
                        IsolationLevel.ReadCommitted, retryToken);
                }, token);

            if (policyResult.FinalException is not null)
            {
                Console.WriteLine(policyResult.FinalException.Message);
                // throw policyResult.FinalException;
            }

            return withdrawProfile;
        }


        public async Task<ProfileEntity> CalcBalanceWithinCashtype(Guid profileId, CancellationToken token)
        {
            await using (var transaction = await _context.BeginTransaction(IsolationLevel.ReadCommitted, token))
            {
                ProfileEntity balance = await _cashProvider.CalcBalanceWithinCashtype(profileId, token);
                return balance;
            }
        }

        public async Task<List<ProfileEntity>> DepositRange(List<ProfileEntity> depositRangeProfileEntities, CancellationToken token)
        {
            List<ProfileEntity> transactionProfileEntities = new();
            foreach (var profileEntity in depositRangeProfileEntities)
            {
                var result = await Deposit(profileEntity, token);
                transactionProfileEntities.Add(result);
            }
            return transactionProfileEntities;

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

            if (filter.UserIds is not null && filter.UserIds.Any())
            {
                predicate = predicate.And(x => filter.UserIds.Contains(x.Id));
            }

            return predicate;
        }

        private Expression<Func<TransactionEntity, bool>> GetFilterExpressionByTransactionEntity(FilterCriteria filter)
        {
            var predicate = PredicateBuilderHelper.True<TransactionEntity>();

            if (filter.UserIds is not null && filter.UserIds.Any())
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