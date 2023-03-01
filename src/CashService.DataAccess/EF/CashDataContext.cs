using CashService.BusinessLogic.Contracts.IRepositories;

namespace CashService.DataAccess.EF
{
    // TODO: Remove all empty lines
    // TODO: Move file to CashService.DataAccess
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