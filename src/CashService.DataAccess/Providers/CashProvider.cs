using CashService.BusinessLogic.Contracts.IProviders;
using CashService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace CashService.DataAccess.Providers
{
    internal class CashProvider : ICashProvider
    {
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

        public async Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token)
        {
            TransactionProfileEntity transactionProfile = new TransactionProfileEntity()
            {
                ProfileId = profileid,
                Transactions = new List<TransactionEntity>()
            };

            await FillTransactionProfileByCashTypeWithSumAmount(profileid, token, transactionProfile);
           
            return transactionProfile;
        }

        private async Task FillTransactionProfileByCashTypeWithSumAmount(Guid profileid, CancellationToken token, TransactionProfileEntity transactionProfile)
        {
            foreach (CashType cashType in Enum.GetValues(typeof(CashType)))
            {
                if (cashType != 0)
                {
                    TransactionEntity transactionEntity = new TransactionEntity
                    {
                        CashType = cashType,
                        TransactionProfileId = profileid,
                        TransactionProfileEntity = transactionProfile
                    };

                    var sumAmount = await SumAmountByCashType(profileid, cashType, token);

                    _logger.LogTrace("SumAmount={sumAmount} with CashType:{cashType}  from database by profile Id:{profileid}", sumAmount, cashType, profileid);

                    transactionEntity.Amount = sumAmount;
                    transactionProfile.Transactions.Add(transactionEntity);
                }
            }
        }

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
