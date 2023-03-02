// TODO: remove unused/sort usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashService.BusinessLogic.Contracts.IProviders
{
    // TODO: change folder name from IProviders to Providers
    // TODO: change file location to CashService.BusinessLogic.Contracts.DataAccess.Providers
    public interface IProvider<T> where T : class
    {
        Task<T> Get(Guid id, CancellationToken cancellationToken);
    }
}
