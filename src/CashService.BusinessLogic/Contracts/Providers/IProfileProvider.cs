using CashService.BusinessLogic.Entities;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface IProfileProvider
    {
        Task<ProfileEntity> Get(Guid id, CancellationToken cancellationToken);
    }
}
