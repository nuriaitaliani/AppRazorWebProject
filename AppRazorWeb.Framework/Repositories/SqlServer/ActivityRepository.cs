using System.Text;
using AppRazorWeb.Framework.Repositories;
using Filters = AppRazorWeb.Framework.Dataservices.Filters.ActivityFilters;
using InsertModel = AppRazorWeb.Framework.Dataservices.Models.ActivityWriteModel;
using ReadModel = AppRazorWeb.Framework.Dataservices.Models.ActivityReadModel;
using UpdateModel = AppRazorWeb.Framework.Dataservices.Models.ActivityWriteModel;

namespace AppRazorWeb.Framework.Repositories.SqlServer
{
    /// <summary>
    /// Implementation activity query repository contract
    /// </summary>
    public class ActivityRepository : IActivityRepository
    {

        #region Deletes

        ///<inheritdoc/>
        public string GetDeleteActivity()
        {
            return @"
            DELETE
            FROM activity
            WHERE id = @Id";
        }

        #endregion Deletes

        #region Inserts

        ///<inheritdoc/>
        public string GetInsertActivity()
        {
            return $@"
INSERT INTO activity
            (id, name, description,
            place, student_name,
            daily_lesson, student_course)
VALUES      (@{nameof(InsertModel.Id)}, @{nameof(InsertModel.Name)},
            @{nameof(InsertModel.Description)}, @{nameof(InsertModel.Place)},
            @{nameof(InsertModel.StudentName)}, @{nameof(InsertModel.DailyLesson)},
            @{nameof(InsertModel.StudentCourse)})";
        }

        #endregion Inserts

        #region Selects

        ///<inheritdoc/>
        public string GetSelectActivity()
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectActivitiesRawQuery());
            queryBuilder.Append(@"
            WHERE activity.id = @Id");

            return queryBuilder.ToString();
        }

        ///<inheritdoc/>
        public string GetSelectActivities(bool filterByName = false, bool filterByPlace = false,
            bool filterByStudentName = false, bool filterByDailyLesson = false,
            bool filterByStudentCourse = false)
        {
            StringBuilder queryBuilder = new StringBuilder(GetSelectActivitiesRawQuery());

            if (filterByName || filterByPlace || filterByStudentName
                || filterByDailyLesson || filterByStudentCourse)
            {
                queryBuilder.Append($@"
                WHERE ");

                if (filterByName)
                {
                    queryBuilder.Append($@" activity.name = @{nameof(Filters.Name)} AND ");
                }

                if (filterByPlace)
                {
                    queryBuilder.Append($@" activity.place = @{nameof(Filters.Place)} AND ");
                }

                if (filterByStudentName)
                {
                    queryBuilder.Append($@" activity.student_name = @{nameof(Filters.StudentName)} AND ");
                }

                if (filterByDailyLesson)
                {
                    queryBuilder.Append($@" activity.daily_lesson = @{nameof(Filters.DailyLesson)} AND ");
                }

                if (filterByStudentCourse)
                {
                    queryBuilder.Append($@" activity.student_course = @{nameof(Filters.StudentCourse)} AND ");
                }

                queryBuilder.Remove(queryBuilder.Length - 4, 3);

            }

            return queryBuilder.ToString();

        }

        #region Helpers

        ///<inheritdoc/>
        public string GetSelectActivitiesRawQuery()
        {
            return $@"
SELECT      activity.id AS ""{nameof(ReadModel.Id)}"", activity.name AS ""{nameof(ReadModel.Name)}"",
            activity.description AS ""{nameof(ReadModel.Description)}"", activity.place AS ""{nameof(ReadModel.Place)}"",
            activity.student_name AS ""{nameof(ReadModel.StudentName)}"", activity.daily_lesson AS ""{nameof(ReadModel.DailyLesson)}"",
            activity.student_course AS ""{nameof(ReadModel.StudentCourse)}""
FROM        activity";
        }

        #endregion Helpers

        #endregion Selects

        #region Updates

        ///<inheritdoc/>
        public string GetUpdateActivity()
        {
            return $@"
UPDATE      activity
SET         name = @{nameof(UpdateModel.Name)}, description = @{nameof(UpdateModel.Description)},
            place = @{nameof(UpdateModel.Place)}, student_name = @{nameof(UpdateModel.StudentName)},
            daily_lesson = @{nameof(UpdateModel.DailyLesson)}, student_course = @{nameof(UpdateModel.StudentCourse)},
            update_date = GETUTCDATE()
WHERE       id = @{nameof(UpdateModel.Id)}";
        }

        #endregion Updates

    }
}
