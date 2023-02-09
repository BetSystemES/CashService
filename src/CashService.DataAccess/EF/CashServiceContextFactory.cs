using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CashService.DataAccess.EF
{
    /// <summary>
    /// Auction service context factory
    /// </summary>
    public class CashServiceContextFactory : IDesignTimeDbContextFactory<CashDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public CashDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CashDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CashDb;User Id=postgres;Password=postgres");

            return new CashDbContext(optionsBuilder.Options);
        }
    }
}
