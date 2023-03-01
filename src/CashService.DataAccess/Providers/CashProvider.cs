using CashService.BusinessLogic.Contracts.IProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CashService.EntityModels.Models;

namespace CashService.DataAccess.Providers
{
    // TODO: remove all empty lines
    // TODO: make service public
    internal class CashProvider : ICashProvider
    {
        // TODO: unused variable _transactionEntities
        private readonly DbSet<TransactionEntity> _transactionEntities;
        private readonly DbSet<TransactionProfileEntity> _transactionProfileEntities;

        private readonly ILogger<CashProvider> _logger;

        public CashProvider(DbSet<TransactionEntity> transactionEntities,
            DbSet<TransactionProfileEntity> transactionProfileEntities,
            ILogger<CashProvider> logger)
        {
           _transactionEntities = transactionEntities;
           _transactionProfileEntities = transactionProfileEntities;
           _logger = logger;
        }


        // TODO: typo in profileid. Should be profileId
        public async Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token)
        {
            var result = await _transactionProfileEntities
                .AsNoTracking()
                .Where(x => x.ProfileId == profileid)
                .Include(y => y.Transactions)
                .FirstOrDefaultAsync(cancellationToken: token);
            return result;
        }

        // TODO: typo in profileid. Should be profileId
        public async Task<TransactionProfileEntity> CalcBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity transactionProfile = new TransactionProfileEntity()
            {
                //Id = Guid.NewGuid(),
                ProfileId = profileid,
                Transactions = new List<TransactionEntity>()
            };

            await FillTransactionProfileByCashTypeWithSumAmount(profileid, token, transactionProfile);
           
            return transactionProfile;
        }

        // TODO: typo in profileid. Should be profileId
        private async Task FillTransactionProfileByCashTypeWithSumAmount(Guid profileid, CancellationToken token, TransactionProfileEntity transactionProfile)
        {
            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    TransactionEntity transactionEntity = new TransactionEntity
                    {
                        CashType = cashType,
                        TransactionId= Guid.NewGuid(),
                        TransactionProfileId = profileid,
                        TransactionProfileEntity = transactionProfile
                    };

                    var sumAmount = await SumAmountByCashType(profileid, cashType, token);

                    // TODO: typo in profileid. Should be profileId
                    _logger.LogTrace("SumAmount={sumAmount} with CashType:{cashType}  from database by profile Id:{profileid}", sumAmount, cashType, profileid);

                    transactionEntity.Amount = sumAmount;
                    transactionProfile.Transactions.Add(transactionEntity);
                }
            }
        }

        // TODO: typo in profileid. Should be profileId
        private async Task<decimal> SumAmountByCashType(Guid profileid, CashType cashType, CancellationToken token)
        {
            var result = await _transactionProfileEntities
                .Where(x => x.ProfileId == profileid)
                .Include(y => y.Transactions)
                .SelectMany(z => z.Transactions.Where(t => t.CashType == cashType))
                .SumAsync(transaction => transaction.Amount, cancellationToken: token);
            return result;
        }
    }
}
