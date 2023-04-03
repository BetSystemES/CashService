using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CashService.BusinessLogic.Contracts.Providers
{
    public interface IProfileProvider
    {
        EntityEntry<ProfileEntity> Entry(ProfileEntity entity);
        Task<ProfileEntity> Get(Guid id, CancellationToken cancellationToken);
    }
}
