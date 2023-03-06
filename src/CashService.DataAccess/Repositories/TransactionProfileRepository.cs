using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Repositories
{
    public class TransactionProfileRepository : SqlRepository<TransactionProfileEntity>, ITransactionProfileRepository
    {

        public TransactionProfileRepository(DbSet<TransactionProfileEntity> entities) : base(entities)
        {
        }
    }
}