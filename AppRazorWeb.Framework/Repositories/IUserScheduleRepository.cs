namespace AppRazorWeb.Framework.Repositories
{
    public interface IUserScheduleRepository
    {

        #region Deletes

        string GetDeleteUserSchedule();

        #endregion Deletes

        #region Inserts

        string GetInsertUserSchedule();

        #endregion Inserts

        #region Selects

        string GetSelectUserSchedule();

        string GetSelectUsersSchedules(bool filterByUserId = false,
            bool filterByUserName = false, bool filterByScheduleId = false,
            bool filterByActivityId = false, bool filterByActivityName = false);

        #region Helpers

        string GetSelectUserScheduleRawQuery();

        #endregion Helpers

        #endregion Selects

        #region Updates

        string GetUpdateUserSchedule();

        #endregion Updates

    }
}
