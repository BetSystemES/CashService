using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.BusinessLogic.Contracts.IProviders;
using CashService.EntityModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Providers
{
    internal class TransactionProfileProvider : IProvider<TransactionProfileEntity>
    {
        private readonly DbSet<TransactionProfileEntity> _entities;

        private readonly ILogger<TransactionProfileProvider> _logger;

        public TransactionProfileProvider(DbSet<TransactionProfileEntity> entities,
            ILogger<TransactionProfileProvider> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<TransactionProfileEntity> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get TransactionProfileEntity item from database by ProfileId:{guid}", guid);
            return result;
        }
    }
}
