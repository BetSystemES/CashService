namespace CashService.BusinessLogic.Contracts.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task AddRange(IEnumerable<T> items, CancellationToken token);
        Task Update(T item, CancellationToken cancellationToken);
    }
}
