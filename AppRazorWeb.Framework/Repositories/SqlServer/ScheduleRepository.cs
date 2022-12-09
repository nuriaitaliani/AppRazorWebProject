using InsertModel = AppRazorWeb.Framework.Dataservices.Models.ScheduleWriteModel;
using UpdateModel = AppRazorWeb.Framework.Dataservices.Models.ScheduleWriteModel;
using ReadModel = AppRazorWeb.Framework.Dataservices.Models.ScheduleReadModel;
using Filters = AppRazorWeb.Framework.Dataservices.Filters.ScheduleFilters;
using System.Text;

namespace AppRazorWeb.Framework.Repositories.SqlServer
{
    public class ScheduleRepository : IScheduleRepository
    {
        #region Deletes

        ///<inheritdoc/>
        public string GetDeleteSchedule()
        {
            return @"
            DELETE
            FROM schedule
            WHERE id = @Id";
        }

        #endregion Deletes

        #region Inserts

        ///<inheritdoc/>
        public string GetInsertSchedule()
        {
            return $@"
INSERT INTO schedule
            (id, start, ""end"",
            activity_id, day_of_week)
VALUES      (@{nameof(InsertModel.Id)}, @{nameof(InsertModel.Start)},
            @{nameof(InsertModel.End)}, @{nameof(InsertModel.ActivityId)},
            @{nameof(InsertModel.DayOfWeek)})";
        }

        #endregion Inserts

        #region Selects

        ///<inheritdoc/>
        public string GetSelectSchedule()
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectScheduleRawQuery());
            queryBuilder.Append(@"
            WHERE schedule.id = @Id");

            return queryBuilder.ToString();
        }

        ///<inheritdoc/>
        public string GetSelectSchedules(bool filterByStart = false, bool filterByEnd = false,
            bool filterByActivity = false, bool filterByDayOfWeek = false)
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectScheduleRawQuery());

            if (filterByStart || filterByEnd || filterByActivity || filterByDayOfWeek)
            {
                queryBuilder.Append($@"
                WHERE ");

                if (filterByStart)
                {
                    queryBuilder.Append($@" schedule.start = @{nameof(Filters.Start)} AND ");
                }

                if (filterByEnd)
                {
                    queryBuilder.Append($@" schedule.""end"" = @{nameof(Filters.End)} AND ");
                }

                if (filterByActivity)
                {
                    queryBuilder.Append($@" schedule.activity_id = @{nameof(Filters.ActivityId)} AND ");
                }

                if (filterByDayOfWeek)
                {
                    queryBuilder.Append($@" schedule.day_of_week = @{nameof(Filters.DayOfWeek)} AND ");
                }

                queryBuilder.Remove(queryBuilder.Length - 4, 3);
            }
            return queryBuilder.ToString();
        }


        #region Helpers

        ///<inheritdoc/>
        public string GetSelectScheduleRawQuery()
        {
            return $@"
SELECT      schedule.id AS ""{nameof(ReadModel.Id)}"", schedule.start AS ""{nameof(ReadModel.Start)}"",
            schedule.""end"" AS ""{nameof(ReadModel.End)}"", schedule.activity_id AS ""{nameof(ReadModel.ActivityId)}"",
            schedule.day_of_week AS ""{nameof(ReadModel.DayOfWeek)}""
FROM        schedule";
        }

        #endregion Helpers

        #endregion Selects

        #region Updates

        ///<inheritdoc/>
        public string GetUpdateSchedule()
        {
            return $@"
UPDATE      schedule
SET         start = @{nameof(UpdateModel.Start)}, ""end"" = @{nameof(UpdateModel.End)},
            activity_id = @{nameof(UpdateModel.ActivityId)}, day_of_week = @{nameof(UpdateModel.DayOfWeek)},
            update_date = GETUTCDATE()
WHERE       id = @{nameof(UpdateModel.Id)}";
        }

        #endregion Updates

    }
}
