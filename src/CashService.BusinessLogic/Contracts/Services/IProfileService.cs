using CashService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CashService.BusinessLogic.Contracts.Services
{
    /// <summary>
    /// Profile service contract.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Creates the profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Task
        /// </returns>
        Task Create(Guid userId, CancellationToken token);

        /// <summary>
        /// Gets the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>The specific profile entity. </returns>
        Task<ProfileEntity> Get(Guid userId, CancellationToken token);

        public EntityEntry Attach(ProfileEntity entity);

        public void Detach(ProfileEntity entity);

        public EntityEntry Entry(ProfileEntity entity);

        Task Update(ProfileEntity entity, CancellationToken token);
    }
}
