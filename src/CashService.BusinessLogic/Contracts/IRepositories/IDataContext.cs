namespace CashService.BusinessLogic.Contracts.IRepositories
{
    public interface IDataContext
    {
        /// <summary>Saves the changes.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   Task
        /// </returns>
        Task SaveChanges(CancellationToken token);
    }
}
