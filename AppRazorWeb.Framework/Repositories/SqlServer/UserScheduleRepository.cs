using System.Text;
using Filters = AppRazorWeb.Framework.Dataservices.Filters.UserScheduleFilters;
using InsertModel = AppRazorWeb.Framework.Dataservices.Models.UserScheduleWriteModel;
using ReadModel = AppRazorWeb.Framework.Dataservices.Models.UserScheduleReadModel;
using UpdateModel = AppRazorWeb.Framework.Dataservices.Models.UserScheduleWriteModel;

namespace AppRazorWeb.Framework.Repositories.SqlServer
{
    public class UserScheduleRepository : IUserScheduleRepository
    {

        #region Deletes

        public string GetDeleteUserSchedule()
        {
            return @"
            DELETE
            FROM userschedule
            WHERE id = @Id";
        }

        #endregion Deletes

        #region Inserts

        public string GetInsertUserSchedule()
        {
            return $@"
INSERT INTO userschedule
            (id, user_id, user_name, schedule_id)
VALUES      (@{nameof(InsertModel.Id)}, @{nameof(InsertModel.UserId)},
             @{nameof(InsertModel.ScheduleId)})";
        }

        #endregion Inserts

        #region Selects

        public string GetSelectUsersSchedules(bool filterByUserId = false, bool filterByUserName = false,
            bool filterByScheduleId = false, bool filterByActivityId = false, bool filterByActivityName = false)
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectUserScheduleRawQuery());

            if (filterByUserId || filterByUserName || filterByScheduleId || filterByActivityId || filterByActivityName)
            {
                queryBuilder.Append($@"
                WHERE ");

                if (filterByUserId)
                {
                    queryBuilder.Append($@" user.id = @{nameof(Filters.UserId)} AND ");
                }

                if (filterByUserName)
                {
                    queryBuilder.Append($@" user.name = @{nameof(Filters.UserName)} AND ");
                }

                if (filterByScheduleId)
                {
                    queryBuilder.Append($@" schedule.id = @{nameof(Filters.ScheduleId)} AND ");
                }

                if (filterByActivityId)
                {
                    queryBuilder.Append($@" activity.id = @{nameof(Filters.ActivityId)} AND ");
                }

                if (filterByActivityName)
                {
                    queryBuilder.Append($@" activity.name = @{nameof(Filters.ActivityName)} AND ");
                }

                queryBuilder.Remove(queryBuilder.Length - 4, 3);
            }
            return queryBuilder.ToString();
        }

        public string GetSelectUserSchedule()
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectUserScheduleRawQuery());
            queryBuilder.Append(@"
            WHERE userschedule.id = @Id");

            return queryBuilder.ToString();
        }

        #region Helpers

        public string GetSelectUserScheduleRawQuery()
        {
            return $@"
SELECT      userschedule.id AS ""{nameof(ReadModel.Id)}"", user_id AS ""{nameof(ReadModel.UserId)}"",
            user.name AS ""{nameof(ReadModel.UserName)}"", schedule_id AS ""{nameof(ReadModel.ScheduleId)}"",
            activity.id AS ""{nameof(ReadModel.ActivityId)}"", activity.name AS ""{nameof(ReadModel.ActivityName)}"",
FROM        userschedule
INNER JOIN  schedule ON schedule.""id"" = userschedule.schedule_id
INNER JOIN  user ON user.""id"" = userschedule.user_id
INNER JOIN  activity on activity.id = schedule.activity_id

";
        }

        #endregion Helpers

        #endregion Selects

        #region Updates

        public string GetUpdateUserSchedule()
        {
            return $@"
UPDATE      userschedule
SET         user_id = @{nameof(UpdateModel.UserId)},
            schedule_id = @{nameof(UpdateModel.ScheduleId)}
WHERE       id = @{nameof(UpdateModel.Id)}";
        }

        #endregion Updates

    }
}
