using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace AppRazorWeb.Framework.Dataservices
{
    /// <summary>
    /// Schedule data service contract
    /// </summary>
    public interface IScheduleDataService
    {

        #region Deletes

        /// <summary>
        /// Deletes a Schedule
        /// </summary>
        /// <param name="scheduleId">Schedule's identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Transaction to be used</param>
        /// <returns>The deletion result</returns>
        Task<int> DeleteSchedule(Guid scheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Deletes

        #region Gets

        /// <summary>
        /// Get a Schedule by a given identifier
        /// </summary>
        /// <param name="scheduleId">Schedule's identifier</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Transaction to be used</param>
        /// <returns>The <see cref="Models.ScheduleReadModel"/></returns>
        Task<Models.ScheduleReadModel> GetSchedule(Guid scheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// Get a list of schedules
        /// Filters may be applied
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <param name="filters">Schedule filters</param>
        /// <returns>The <see cref="Models.ScheduleReadModel"/></returns>
        Task<List<Models.ScheduleReadModel>> GetSchedules(IDbConnection dbConnection, IDbTransaction dbTransaction = null,
                Filters.ScheduleFilters filters = null);

        #endregion Gets

        #region Inserts

        /// <summary>
        /// Insert a schedule
        /// </summary>
        /// <param name="schedule">Schedule to be inserted</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        Task InsertSchedule(Models.ScheduleWriteModel schedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Inserts

        #region Updates

        /// <summary>
        /// Update a schedule
        /// </summary>
        /// <param name="schedule">Schedule to be updated</param>
        /// <param name="dbConnection">Database connection</param>
        /// <param name="dbTransaction">Database transaction</param>
        /// <returns></returns>
        Task UpdateSchedule(Models.ScheduleWriteModel schedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion Updates

    }

}

