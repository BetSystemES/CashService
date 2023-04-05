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

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>EntityEntry</returns>
        public EntityEntry Attach(ProfileEntity entity);

        /// <summary>
        /// Detaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Detach(ProfileEntity entity);

        /// <summary>
        /// Entries the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>EntityEntry</returns>
        public EntityEntry Entry(ProfileEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        Task Update(ProfileEntity entity, CancellationToken token);
    }
}
