namespace CashService.BusinessLogic.Contracts.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task<T> Get(Guid guid, CancellationToken cancellationToken);
        Task Update(T item, CancellationToken cancellationToken);
    }
}
