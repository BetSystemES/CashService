using CashService.BusinessLogic.Contracts.IRepositories;

namespace CashService.DataAccess.EF
{
    public class CashDataContext : IDataContext
    {
        private readonly CashDbContext _profileDbContext;

        public CashDataContext(CashDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }

        /// <summary>Saves the changes.</summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>
        ///   Save changes result
        /// </returns>
        public Task SaveChanges(CancellationToken token)
        {
            return _profileDbContext.SaveChangesAsync(token);
        }
    }

}