using System.Text;
using Filters = AppRazorWeb.Framework.Dataservices.Filters.UserScheduleFilters;
using InsertModel = AppRazorWeb.Framework.Dataservices.Models.UserScheduleWriteModel;
using ReadModel = AppRazorWeb.Framework.Dataservices.Models.UserScheduleReadModel;

namespace AppRazorWeb.Framework.Repositories.SqlServer
{
    public class UserScheduleRepository : IUserScheduleRepository
    {

        #region Deletes

        public string GetDeleteUserScheduleByUserId()
        {
            return @"
            DELETE
            FROM ""user_schedule""
            WHERE user_id = @Id";
        }

        public string GetDeleteUserScheduleByScheduleId()
        {
            return @"
            DELETE
            FROM ""user_schedule""
            WHERE schedule_id = @Id";
        }

        #endregion Deletes

        #region Inserts

        public string GetInsertUserSchedule()
        {
            return $@"
INSERT INTO ""user_schedule"" (user_id, schedule_id)
VALUES      (@{nameof(InsertModel.UserId)},
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
                    queryBuilder.Append($@" ""user_schedule"".user_id = @{nameof(Filters.UserId)} AND ");
                }

                if (filterByUserName)
                {
                    queryBuilder.Append($@" ""user"".name = @{nameof(Filters.UserName)} AND ");
                }

                if (filterByScheduleId)
                {
                    queryBuilder.Append($@" ""user_schedule"".schedule_id = @{nameof(Filters.ScheduleId)} AND ");
                }

                if (filterByActivityId)
                {
                    queryBuilder.Append($@" ""schedule"".activity_id = @{nameof(Filters.ActivityId)} AND ");
                }

                if (filterByActivityName)
                {
                    queryBuilder.Append($@" ""activity"".name = @{nameof(Filters.ActivityName)} AND ");
                }

                queryBuilder.Remove(queryBuilder.Length - 4, 3);
            }
            return queryBuilder.ToString();
        }

        #region Helpers

        public string GetSelectUserScheduleRawQuery()
        {
            //Falta start, end y dayOfweek de schedule
            return $@"
SELECT      ""user_schedule"".user_id AS ""{nameof(ReadModel.UserId)}"",
            ""user"".name AS ""{nameof(ReadModel.UserName)}"",
            ""user_schedule"".schedule_id AS ""{nameof(ReadModel.ScheduleId)}"",
            ""schedule"".activity_id AS ""{nameof(ReadModel.ActivityId)}"",
            ""activity"".name AS ""{nameof(ReadModel.ActivityName)}"",
            ""schedule"".start AS ""{nameof(ReadModel.ScheduleStart)}"",
            ""schedule"".end AS ""{nameof(ReadModel.ScheduleEnd)}"",
            ""schedule"".day_of_week AS {nameof(ReadModel.ScheduleDayOfWeek)}
FROM        ""user_schedule""
INNER JOIN  ""schedule"" ON ""schedule"".id = ""user_schedule"".schedule_id
INNER JOIN  ""user"" ON ""user"".id = ""user_schedule"".user_id
INNER JOIN  ""activity"" on ""activity"".id = ""schedule"".activity_id
";
        }

        #endregion Helpers

        #endregion Selects

        
    }
}
