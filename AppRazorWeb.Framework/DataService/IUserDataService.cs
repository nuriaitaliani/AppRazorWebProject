using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace AppRazorWeb.Framework.Dataservices
{
    /// <summary>
    /// User data service contract
    /// </summary>
    public interface IUserDataService
    {

        #region Deletes

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Transaction to be used</param>
        /// <returns>The deletion result</returns>
        Task<int> DeleteUser(Guid userId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Deletes

        #region Gets

        /// <summary>
        /// Get a User by a given identifier
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Transaction to be used</param>
        /// <returns>The <see cref="Models.UserReadModel"/></returns>
        Task<Models.UserReadModel> GetUser(Guid userId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// Get a list of users
        /// Filters may be applied
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <param name="filters">User filters</param>
        /// <returns>The <see cref="Models.UserReadModel"/></returns>
        Task<List<Models.UserReadModel>> GetUsers(IDbConnection dbConnection, IDbTransaction dbTransaction = null,
                Filters.UserFilters filters = null);

        #endregion Gets

        #region Inserts

        /// <summary>
        /// Insert a user
        /// </summary>
        /// <param name="user">User to be inserted</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        Task InsertUser(Models.UserWriteModel user, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Inserts

        #region Updates

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">User to be updated</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns></returns>
        Task UpdateUser(Models.UserWriteModel user, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Updates

    }
}
