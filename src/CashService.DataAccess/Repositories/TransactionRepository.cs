using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Repositories
{
    public class TransactionRepository : SqlRepository<TransactionEntity>, ITransactionRepository
    {

        public TransactionRepository(DbSet<TransactionEntity> entities) : base(entities)
        {
        }
    }
}