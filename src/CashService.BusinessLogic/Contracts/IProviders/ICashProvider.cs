using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.BusinessLogic.Models;

namespace CashService.BusinessLogic.Contracts.IProviders
{
    public interface ICashProvider
    {
        Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token);

        Task<TransactionProfileEntity> CalcBalance(Guid profileid, CancellationToken token);
    }
}
