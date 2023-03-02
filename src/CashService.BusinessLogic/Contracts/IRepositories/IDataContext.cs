﻿namespace CashService.BusinessLogic.Contracts.IRepositories
{
    // TODO: change file location to CashService.BusinessLogic.Contracts.DataAccess
    public interface IDataContext
    {
        /// <summary>Saves the changes.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   Task
        /// </returns>
        Task SaveChanges(CancellationToken token);
    }
}
