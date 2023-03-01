// TODO: remove unused/sort usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashService.EntityModels.Models;

namespace CashService.BusinessLogic.Contracts.IProviders
{
    // TODO: change file location to CashService.DataAccess.Contracts.Providers
    // TODO: inherit ICashProvider from IProvider
    public interface ICashProvider
    {
        // TODO: typo in profileid. Should be profileId
        Task<TransactionProfileEntity> GetBalance(Guid profileid, CancellationToken token);

        // TODO: typo in profileid. Should be profileId
        Task<TransactionProfileEntity> CalcBalance(Guid profileid, CancellationToken token);
    }
}
