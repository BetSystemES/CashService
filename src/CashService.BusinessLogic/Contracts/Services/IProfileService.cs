using CashService.BusinessLogic.Entities;

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
        Task CreateProfile(Guid userId, CancellationToken token); 
    }
}
