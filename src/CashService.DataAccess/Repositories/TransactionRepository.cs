using CashService.BusinessLogic.Contracts.Repositories;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashService.DataAccess.Repositories
{
    public class TransactionRepository : SqlRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(CashDbContext context) : base(context)
        {
        }
    }
}