namespace AppRazorWeb.Framework.Repositories
{
    public interface IUserScheduleRepository
    {

        #region Deletes

        string GetDeleteUserScheduleByUserId();

        string GetDeleteUserScheduleByScheduleId();

        #endregion Deletes

        #region Inserts

        string GetInsertUserSchedule();

        #endregion Inserts

        #region Selects

        string GetSelectUsersSchedules(bool filterByUserId = false,
            bool filterByUserName = false, bool filterByScheduleId = false,
            bool filterByActivityId = false, bool filterByActivityName = false);

        #region Helpers

        string GetSelectUserScheduleRawQuery();

        #endregion Helpers

        #endregion Selects

    }
}
