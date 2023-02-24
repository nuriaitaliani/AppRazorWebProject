using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Dataservices.Models;
using AppRazorWeb.Framework.Repositories;
using Dapper;

namespace AppRazorWeb.Framework.Dataservices
{
    public class User_ScheduleDataService : IUserScheduleDataService
    {

        #region Fields

        private readonly IUserScheduleRepository _userScheduleRepository;
        private readonly IConnectionCrud _connectionCrud;

        #endregion Fields

        #region Constructor

        public User_ScheduleDataService(IUserScheduleRepository userScheduleRepository, IConnectionCrud connectionCrud)
        {
            _connectionCrud = connectionCrud;
            _userScheduleRepository= userScheduleRepository;
        }

        #endregion Constructor

        #region Deletes

        public async Task<int> DeleteUserScheduleByUserId(Guid userId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", userId, DbType.Guid);

            string query = _userScheduleRepository.GetDeleteUserScheduleByUserId();

            return await _connectionCrud.Delete(dbConnection, query, parameters, dbTransaction);
        }

        public async Task<int> DeleteUserScheduleByScheduleId(Guid scheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", scheduleId, DbType.Guid);

            string query = _userScheduleRepository.GetDeleteUserScheduleByScheduleId();

            return await _connectionCrud.Delete(dbConnection, query, parameters, dbTransaction);
        }

        #endregion Deletes

        #region Gets

        public async Task<List<UserScheduleReadModel>> GetUserSchedules(IDbConnection dbConnection,
            IDbTransaction dbTransaction = null, UserScheduleFilters filters = null)
        {
            List<UserScheduleReadModel> userSchedules;

            if (filters == null)
            {
                string query = _userScheduleRepository.GetSelectUsersSchedules();

                userSchedules = (await _connectionCrud.Load<UserScheduleReadModel>(dbConnection, query, dbTransaction)).ToList();
            }
            else
            {
                DynamicParameters parameters = new DynamicParameters();

                bool filterByUserId = Guid.Empty != filters.UserId;
                if(filterByUserId)
                {
                    parameters.Add(nameof(UserScheduleFilters.UserId), filters.UserId, DbType.Guid);
                }

                bool filterByUserName = !string.IsNullOrWhiteSpace(filters.UserName);
                if (filterByUserName)
                {
                    parameters.Add(nameof(UserScheduleFilters.UserName), filters.UserName, DbType.String);
                }

                bool filterByActivityId = Guid.Empty != filters.ActivityId;
                if (filterByActivityId)
                {
                    parameters.Add(nameof(UserScheduleFilters.ActivityId), filters.ActivityId, DbType.Guid);
                }

                bool filterByActivityName = !string.IsNullOrWhiteSpace(filters.ActivityName);
                if (filterByActivityName)
                {
                    parameters.Add(nameof(UserScheduleFilters.ActivityName), filters.ActivityName, DbType.String);
                }

                bool filterByScheduleId = Guid.Empty != filters.ScheduleId;
                if (filterByScheduleId)
                {
                    parameters.Add(nameof(UserScheduleFilters.ScheduleId), filters.ScheduleId, DbType.Guid);
                }

                string query = _userScheduleRepository.GetSelectUsersSchedules(filterByUserId, filterByUserName,
                    filterByActivityId, filterByActivityName, filterByScheduleId);

                userSchedules = (await _connectionCrud.Load<UserScheduleReadModel, DynamicParameters>
                    (dbConnection, query, parameters, dbTransaction)).ToList();

                
            }
            return userSchedules;
        }

        #endregion Gets

        #region Inserts

        public async Task InsertUserSchedule(UserScheduleWriteModel userSchedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = GetUserScheduleDynamicParameters(userSchedule);

            string query = _userScheduleRepository.GetInsertUserSchedule();

            await _connectionCrud.Save<UserScheduleWriteModel, DynamicParameters>
                (dbConnection, query, parameters, dbTransaction);
        }

        #endregion Inserts

        #region Private Helpers

        private DynamicParameters GetUserScheduleDynamicParameters(UserScheduleWriteModel userSchedule)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(nameof(UserScheduleWriteModel.UserId),userSchedule.UserId,DbType.Guid);
            parameters.Add(nameof(UserScheduleWriteModel.ScheduleId),userSchedule.ScheduleId, DbType.Guid);

            return parameters;
        }

        #endregion Private Helpers

    }
}
