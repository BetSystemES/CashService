using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashService.DataAccess.Providers
{
    public class ProfileProvider : IProfileProvider
    {

        private readonly DbSet<ProfileEntity> _entities;

        private readonly ILogger<ProfileProvider> _logger;

        public ProfileProvider(DbSet<ProfileEntity> entities, ILogger<ProfileProvider> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<ProfileEntity> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get ProfileEntity item from database by ProfileId:{guid}", guid);
            return result;
        }
    }
}
