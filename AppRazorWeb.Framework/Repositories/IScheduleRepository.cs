namespace AppRazorWeb.Framework.Repositories
{
    public interface IScheduleRepository
    {

        #region Deletes

        /// <summary>
        /// Get the query to delete a schedule
        /// </summary>
        /// <returns>The query</returns>
        string GetDeleteSchedule();

        #endregion Deletes

        #region Inserts

        /// <summary>
        /// Get the query to insert a schedule
        /// </summary>
        /// <returns>The query</returns>
        string GetInsertSchedule();

        #endregion Inserts

        #region Selects

        /// <summary>
        /// Get the query to select a schedule by identifier
        /// </summary>
        /// <returns>The query</returns>
        string GetSelectSchedule();

        /// <summary>
        /// Get the query select a schedule
        /// </summary>
        /// <param name="filterByStart">Filter by start</param>
        /// <param name="filterByEnd">Filter by end</param>
        /// <param name="filterByActivity">Filter by activity</param>
        /// <param name="filterByDayOfWeek">Filter by day of week</param>
        /// <returns>The query</returns>
        string GetSelectSchedules(bool filterByStart = false, bool filterByEnd = false,
            bool filterByActivity = false, bool filterByDayOfWeek = false);

        #region Helpers

        /// <summary>
        /// Get the raw query to select schedules
        /// </summary>
        /// <returns>The query</returns>
        string GetSelectScheduleRawQuery();

        #endregion Helpers

        #endregion Selects

        #region Updates

        /// <summary>
        /// Get the query to update a schedule
        /// </summary>
        /// <returns>The query</returns>
        string GetUpdateSchedule();

        #endregion Updates
    }
}
