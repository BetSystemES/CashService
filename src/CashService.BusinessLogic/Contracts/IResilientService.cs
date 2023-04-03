using System.Data;

namespace CashService.BusinessLogic.Contracts
{
    public interface IResilientService
    {
        Task ExecuteAsync(Func<Task> action, IsolationLevel isolationLevel, CancellationToken ctoken);
    }
}
