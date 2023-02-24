using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.BusinessLogic.Contracts.IProviders
{
    public interface IProvider<T> where T : class
    {
        Task<T> Get(Guid id, CancellationToken cancellationToken);
    }
}
