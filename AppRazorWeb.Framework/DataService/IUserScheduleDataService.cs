using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace AppRazorWeb.Framework.Dataservices
{
    /// <summary>
    /// User_Schedule data service contract
    /// </summary>
    public interface IUserScheduleDataService
    {

        #region Deletes

        /// <summary>
        /// Deletes an user schedule
        /// </summary>
        /// <param name="userScheduleId">User schedule identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The deletion result</returns>
        Task<int> DeleteUserSchedules(Guid userScheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Deletes

        #region Gets

        /// <summary>
        /// Get an user schedule by a given identifier
        /// </summary>
        /// <param name="userScheduleId">user schedule identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The <see cref="Models.User_ScheduleReadModel"/></returns>
        Task<Models.UserScheduleReadModel> GetUserSchedule(Guid userScheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// Get a list of user schedules
        /// Filters may be applied
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <param name="filters">User Schedules filters</param>
        /// <returns>The <see cref="Models.UserScheduleReadModel"/></returns>
        Task<List<Models.UserScheduleReadModel>> GetUserSchedules(IDbConnection dbConnection, IDbTransaction dbTransaction = null,
            Filters.UserScheduleFilters filters = null);

        #endregion Gets

        #region Inserts

        /// <summary>
        /// Insert an user schedule
        /// </summary>
        /// <param name="userSchedule">User schedule to be inserted</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The <see cref="Models.UserScheduleWriteModel"/></returns>
        Task InsertUserSchedule(Models.UserScheduleWriteModel userSchedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Inserts

        #region Updates

        /// <summary>
        /// Update an user schedule
        /// </summary>
        /// <param name="userSchedule">User schedule to be updated</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns>The <see cref="Models.UserScheduleWriteModel"/></returns>
        Task UpdateUserSchedule(Models.UserScheduleWriteModel userSchedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Updates

    }
}
