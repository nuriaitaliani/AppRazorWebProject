using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Dataservices.Models;
using AppRazorWeb.Framework.Repositories;

namespace AppRazorWeb.Framework.Dataservices
{
    public class ActivityDataService : IActivityDataService
    {

        #region Fields

        private readonly IActivityRepository _activityRepository;
        private readonly IConnectionCrud _connectionCrud;

        #endregion Fields

        #region Constructor

        public ActivityDataService(IActivityRepository activityRepository, IConnectionCrud connectionCrud)
        {
            _activityRepository = activityRepository;
            _connectionCrud = connectionCrud;
        }

        #endregion Constructor

        #region IActivityDataService implementation

        #region Deletes

        public async Task<int> DeleteActivity(Guid activityId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", activityId, DbType.Guid);

            string query = _activityRepository.GetDeleteActivity();

            return await _connectionCrud.Delete(dbConnection, query, parameters, dbTransaction);
        }

        #endregion Deletes

        #region Inserts

        public async Task InsertActivity(ActivityWriteModel activity, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = GetActivityDynamicParameters(activity);

            string query = _activityRepository.GetInsertActivity();

            await _connectionCrud.Save<ActivityWriteModel, DynamicParameters>
                (dbConnection, query, parameters, dbTransaction);
        }

        #endregion Inserts

        #region Gets

        public async Task<ActivityReadModel> GetActivity(Guid activityId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", activityId, DbType.Guid);

            string query = _activityRepository.GetSelectActivity();

            ActivityReadModel activity = (await _connectionCrud
                .Load<ActivityReadModel, DynamicParameters>(dbConnection, query, parameters, dbTransaction))
                .FirstOrDefault();

            return activity;
        }

        public async Task<List<ActivityReadModel>> GetActivities(IDbConnection dbConnection, IDbTransaction dbTransaction = null,
            ActivityFilters filters = null)
        {
            List<ActivityReadModel> activities;
            if (filters == null)
            {
                string query = _activityRepository.GetSelectActivities();

                activities = (await _connectionCrud.Load<ActivityReadModel>(dbConnection, query, dbTransaction))
                    .ToList();
            }
            else
            {
                DynamicParameters parameters = new DynamicParameters();

                bool filterByName = !string.IsNullOrWhiteSpace(filters.Name);
                if (filterByName)
                {
                    parameters.Add(nameof(ActivityFilters.Name), filters.Name, DbType.String);
                }

                bool filterByPlace = !string.IsNullOrWhiteSpace(filters.Place);
                if (filterByPlace)
                {
                    parameters.Add(nameof(ActivityFilters.Place), filters.Place, DbType.String);
                }

                bool filterByStudentName = !string.IsNullOrWhiteSpace(filters.StudentName);
                if (filterByStudentName)
                {
                    parameters.Add(nameof(ActivityFilters.StudentName), filters.StudentName, DbType.String);
                }

                bool filterByDailyLesson = !string.IsNullOrWhiteSpace(filters.DailyLesson);
                if (filterByDailyLesson)
                {
                    parameters.Add(nameof(ActivityFilters.DailyLesson), filters.DailyLesson, DbType.String);
                }

                bool filterByStudentCourse = filters.StudentCourse != null;
                if (filterByStudentCourse)
                {
                    parameters.Add(nameof(ActivityFilters.StudentCourse), filters.StudentCourse, DbType.Int64);
                }

                string query = _activityRepository.GetSelectActivities(filterByName, filterByPlace, filterByStudentName,
                    filterByDailyLesson, filterByStudentCourse);

                activities = (await _connectionCrud.Load<ActivityReadModel, DynamicParameters>(dbConnection, query, parameters, dbTransaction))
                    .ToList();
            }
            return activities;
        }

        #endregion Gets

        #region Updates

        public async Task UpdateActivity(ActivityWriteModel activity, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();

            string query = _activityRepository.GetUpdateActivity();

            await _connectionCrud.Save<ActivityWriteModel, DynamicParameters>
                (dbConnection, query, parameters, dbTransaction);
        }

        #endregion Updates

        #endregion IActivityDataService implementation

        #region Private Helpers

        private DynamicParameters GetActivityDynamicParameters(ActivityWriteModel activity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(nameof(ActivityWriteModel.Id), activity.Id, DbType.Guid);
            parameters.Add(nameof(ActivityWriteModel.Name), activity.Name, DbType.String);
            parameters.Add(nameof(ActivityWriteModel.Description), activity.Description, DbType.String);
            parameters.Add(nameof(ActivityWriteModel.Place), activity.Place, DbType.String);
            parameters.Add(nameof(ActivityWriteModel.StudentName), activity.StudentName, DbType.String);
            parameters.Add(nameof(ActivityWriteModel.DailyLesson), activity.DailyLesson, DbType.String);
            parameters.Add(nameof(ActivityWriteModel.StudentCourse), activity.StudentCourse, DbType.Int64);

            return parameters;
        }

        #endregion Private Helpers


    }
}
