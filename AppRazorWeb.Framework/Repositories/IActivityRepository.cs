namespace AppRazorWeb.Framework.Repositories
{
    public interface IActivityRepository
    {

        #region Deletes

        /// <summary>
        /// Get the query to delete an activity
        /// </summary>
        /// <returns>The query</returns>
        string GetDeleteActivity();

        #endregion Deletes

        #region Inserts

        /// <summary>
        /// Get the query to insert an activity
        /// </summary>
        /// <returns>The query</returns>
        string GetInsertActivity();

        #endregion Inserts

        #region Selects

        /// <summary>
        ///Get the query to select an activity by identifier 
        /// </summary>
        /// <returns>The query</returns>
        string GetSelectActivity();

        /// <summary>
        /// Get the query to select an activity
        /// Filters may be applied
        /// </summary>
        /// <param name="filterByName">Filter by name</param>
        /// <param name="filterByPlace">Filter by place</param>
        /// <param name="filterByStudentName">Filter by student's name</param>
        /// <param name="filterByDailyLesson">Filter by daily lesson</param>
        /// <param name="filterByStudentCourse">Filter by student's course</param>
        /// <returns>The query</returns>
        string GetSelectActivities(bool filterByName = false, bool filterByPlace = false,
            bool filterByStudentName = false, bool filterByDailyLesson = false,
            bool filterByStudentCourse = false);

        #region Helpers

        /// <summary>
        /// Get the raw query to select activities
        /// </summary>
        /// <returns>The query</returns>
        string GetSelectActivitiesRawQuery();

        #endregion Helpers

        #endregion Selects

        #region Updates

        /// <summary>
        /// Get the query to update an activity
        /// </summary>
        /// <returns>The query</returns>
        string GetUpdateActivity();

        #endregion Updates

    }
}
