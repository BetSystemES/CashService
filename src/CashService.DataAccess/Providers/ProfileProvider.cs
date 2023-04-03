using CashService.BusinessLogic.Contracts.Providers;
using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Polly;

namespace CashService.DataAccess.Providers
{
    public class ProfileProvider : IProfileProvider
    {
        private readonly CashDbContext _context;

        private readonly DbSet<ProfileEntity> _entities;

        private readonly ILogger<ProfileProvider> _logger;

        public ProfileProvider(CashDbContext context, ILogger<ProfileProvider> logger)
        {
            _context = context;
            _entities = context.Set<ProfileEntity>();
            _logger = logger;
        }

        public EntityEntry<ProfileEntity> Entry(ProfileEntity entity)
        {
            return _context.Entry(entity);
        }

        public async Task<ProfileEntity> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get ProfileEntity item from database by ProfileId:{guid}", guid);
            return result;
        }
    }
}
