using CashService.BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CashService.DataAccess
{
    public class ResilientService : IResilientService
    {
        private readonly IDataContext _context;

        public ResilientService(IDataContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Func<Task> action, IsolationLevel isolationLevel, CancellationToken ctoken)
        {
            var strategy = _context.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.BeginTransaction(isolationLevel, ctoken);
                await action();
                await transaction.CommitAsync(ctoken);
            });
        }
    }
}
