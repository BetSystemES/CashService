namespace CashService.BusinessLogic.Contracts.IRepositories
{
    // TODO: change folder name from IRepositories to Repositories
    // TODO: change file location to CashService.BusinessLogic.Contracts.Repositories
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task AddRange(IEnumerable<T> items, CancellationToken token);
        Task Update(T item, CancellationToken cancellationToken);
    }
}
