using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Dataservices.Models;

namespace AppRazorWeb.Framework.Dataservices
{
    public class User_ScheduleDataService : IUserScheduleDataService
    {

        #region Fields

        private readonly IUserScheduleDataService _service;
        private readonly IConnectionCrud _connectionCrud;

        #endregion Fields

        #region Constructor

        public User_ScheduleDataService(IUserScheduleDataService user_ScheduleDataService, IConnectionCrud connectionCrud)
        {
            _connectionCrud = connectionCrud;
            _service = user_ScheduleDataService;
        }

        #endregion Constructor

        #region Deletes

        public Task<int> DeleteUserSchedules(Guid userScheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }

        #endregion Deletes

        #region Gets

        public Task<UserScheduleReadModel> GetUserSchedule(Guid userScheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserScheduleReadModel>> GetUserSchedules(IDbConnection dbConnection, IDbTransaction dbTransaction = null, UserScheduleFilters filters = null)
        {
            throw new NotImplementedException();
        }

        #endregion Gets

        #region Inserts

        public Task InsertUserSchedule(UserScheduleWriteModel userSchedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }

        #endregion Inserts

        #region Updates

        public Task UpdateUserSchedule(UserScheduleWriteModel userSchedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }

        #endregion Updates

    }
}
