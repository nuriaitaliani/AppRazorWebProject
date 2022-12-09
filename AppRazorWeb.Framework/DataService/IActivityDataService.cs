using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AppRazorWeb.Framework.Dataservices
{
    /// <summary>
    /// Activity data service contract
    /// </summary>
    public interface IActivityDataService
    {

        #region Deletes

        /// <summary>
        /// Deletes an activity
        /// </summary>
        /// <param name="activityId">Activity identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The deletion result</returns>
        Task<int> DeleteActivity(Guid activityId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Deletes

        #region Gets

        /// <summary>
        /// Get an activity by a given identifier
        /// </summary>
        /// <param name="activityId">Activity identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The <see cref="Models.ActivityReadModel"/></returns>
        Task<Models.ActivityReadModel> GetActivity(Guid activityId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// Get a list of activities
        /// Filters may be applied
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <param name="filters">Activity filters</param>
        /// <returns>The <see cref="Models.ActivityReadModel"/></returns>
        Task<List<Models.ActivityReadModel>> GetActivities(IDbConnection dbConnection, IDbTransaction dbTransaction = null,
            Filters.ActivityFilters filters = null);

        #endregion Gets

        #region Inserts

        /// <summary>
        /// Insert an activity
        /// </summary>
        /// <param name="activity">Activity to be inserted</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The <see cref="Models.ActivityWriteModel"/></returns>
        Task InsertActivity(Models.ActivityWriteModel activity, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Inserts

        #region Updates

        /// <summary>
        /// Update an activity
        /// </summary>
        /// <param name="activity">Activity to be updated</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The <see cref="Models.ActivityWriteModel"/></returns>
        Task UpdateActivity(Models.ActivityWriteModel activity, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Updates

    }
}
