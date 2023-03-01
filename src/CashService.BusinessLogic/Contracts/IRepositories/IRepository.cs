namespace CashService.BusinessLogic.Contracts.IRepositories
{
    // TODO: change file location to CashService.DataAccess.Contracts.Repositories
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task AddRange(IEnumerable<T> items, CancellationToken token);
        Task Update(T item, CancellationToken cancellationToken);
    }
}
