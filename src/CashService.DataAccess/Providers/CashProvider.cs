using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Entities;
using CashService.BusinessLogic.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Providers
{
    public class CashProvider : ICashProvider
    {
        private readonly DbSet<TransactionEntity> _transactionEntities;
        private readonly DbSet<ProfileEntity> _transactionProfileEntities;

        private readonly ILogger<CashProvider> _logger;

        public CashProvider(DbSet<TransactionEntity> transactionEntities,
            DbSet<ProfileEntity> transactionProfileEntities,
            ILogger<CashProvider> logger)
        {
           _transactionEntities = transactionEntities;
           _transactionProfileEntities = transactionProfileEntities;
           _logger = logger;
        }

        public async Task<ProfileEntity> GetBalance(Guid profileId, CancellationToken token)
        {
            var result = await _transactionProfileEntities
                .AsNoTracking()
                .Where(x => x.Id == profileId)
                .Include(y => y.Transactions)
                .FirstOrDefaultAsync(cancellationToken: token);
            return result;
        }

        public async Task<ProfileEntity> CalcBalance(Guid profileId, CancellationToken token)
        {
            ProfileEntity profile = new ProfileEntity()
            {
                //Id = Guid.NewGuid(),
                Id = profileId,
                Transactions = new List<TransactionEntity>()
            };

            await FillTransactionProfileByCashTypeWithSumAmount(profileId, token, profile);
           
            return profile;
        }

        private async Task FillTransactionProfileByCashTypeWithSumAmount(Guid profileId, CancellationToken token, ProfileEntity profile)
        {
            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    TransactionEntity transactionEntity = new TransactionEntity
                    {
                        CashType = cashType,
                        Id= Guid.NewGuid(),
                        ProfileId = profileId,
                        ProfileEntity = profile
                    };

                    var sumAmount = await SumAmountByCashType(profileId, cashType, token);

                    _logger.LogTrace("SumAmount={sumAmount} with CashType:{cashType}  from database by profileId:{profileId}", sumAmount, cashType, profileId);

                    transactionEntity.Amount = sumAmount;
                    profile.Transactions.Add(transactionEntity);
                }
            }
        }

        private async Task<decimal> SumAmountByCashType(Guid profileId, CashType cashType, CancellationToken token)
        {
            var result = await _transactionProfileEntities
                .Where(x => x.Id == profileId)
                .Include(y => y.Transactions)
                .SelectMany(z => z.Transactions.Where(t => t.CashType == cashType))
                .SumAsync(transaction => transaction.Amount, cancellationToken: token);
            return result;
        }
    }
}
