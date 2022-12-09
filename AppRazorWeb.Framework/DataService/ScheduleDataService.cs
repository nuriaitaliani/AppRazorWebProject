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
    public class ScheduleDataService : IScheduleDataService
    {

        #region Fields

        private readonly IScheduleRepository _scheduleRepository;
        private readonly IConnectionCrud _connectionCrud;

        #endregion Fields

        #region Constructors

        public ScheduleDataService(IScheduleRepository scheduleRepository, IConnectionCrud connectionCrud)
        {
            _connectionCrud = connectionCrud;
            _scheduleRepository = scheduleRepository;
        }

        #endregion Constructors

        #region Deletes

        public async Task<int> DeleteSchedule(Guid scheduleId, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", scheduleId, DbType.Guid);

            string query = _scheduleRepository.GetDeleteSchedule();

            return await _connectionCrud.Delete(dbConnection, query, parameters, dbTransaction);
        }

        #endregion Deletes

        #region Gets

        public async Task<ScheduleReadModel> GetSchedule(Guid scheduleId, IDbConnection dbConnection,
            IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", scheduleId, DbType.Guid);

            string query = _scheduleRepository.GetSelectSchedule();

            ScheduleReadModel schedule = (await _connectionCrud
            .Load<ScheduleReadModel, DynamicParameters>(dbConnection, query, parameters, dbTransaction))
                .FirstOrDefault();

            return schedule;

        }

        public async Task<List<ScheduleReadModel>> GetSchedules(IDbConnection dbConnection, IDbTransaction dbTransaction = null,
            ScheduleFilters filters = null)
        {
            List<ScheduleReadModel> schedules;
            if (filters == null)
            {
                string query = _scheduleRepository.GetSelectSchedules();

                schedules = (await _connectionCrud.Load<ScheduleReadModel>(dbConnection, query, dbTransaction))
                    .ToList();
            }
            else
            {
                DynamicParameters parameters = new DynamicParameters();

                if (filters.Start.HasValue)
                {
                    parameters.Add(nameof(ScheduleFilters.Start), filters.Start, DbType.Time);
                }

                if (filters.End.HasValue)
                {
                    parameters.Add(nameof(ScheduleFilters.End), filters.End, DbType.Time);
                }

                if (filters.DayOfWeek.HasValue)
                {
                    parameters.Add(nameof(ScheduleFilters.DayOfWeek), filters.DayOfWeek, DbType.Byte);
                }

                if (filters.ActivityId.HasValue)
                {
                    parameters.Add(nameof(ScheduleFilters.ActivityId), filters.ActivityId, DbType.Guid);
                }
                string query = _scheduleRepository.GetSelectSchedules(filters.Start.HasValue, filters.End.HasValue,
                    filters.ActivityId.HasValue, filters.DayOfWeek.HasValue);

                schedules = (await _connectionCrud.Load<ScheduleReadModel, DynamicParameters>(dbConnection, query, parameters, dbTransaction))
                    .ToList();
            }
            return schedules;
        }

        #endregion Gets

        #region Inserts

        public async Task InsertSchedule(ScheduleWriteModel schedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = GetScheduleDynamicParameters(schedule);

            string query = _scheduleRepository.GetInsertSchedule();

            await _connectionCrud.Save<UserScheduleWriteModel, DynamicParameters>
                (dbConnection, query, parameters, dbTransaction);
        }

        #endregion Inserts

        #region Updates

        public async Task UpdateSchedule(ScheduleWriteModel schedule, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters = GetScheduleDynamicParameters(schedule);

            string query = _scheduleRepository.GetUpdateSchedule();

            await _connectionCrud.Save<ScheduleWriteModel, DynamicParameters>
                (dbConnection, query, parameters, dbTransaction);
        }

        #endregion Updates

        #region Private Helpers

        private DynamicParameters GetScheduleDynamicParameters(ScheduleWriteModel schedule)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(nameof(ScheduleWriteModel.Id), schedule.Id, DbType.Guid);
            parameters.Add(nameof(ScheduleWriteModel.Start), schedule.Start, DbType.Time);
            parameters.Add(nameof(ScheduleWriteModel.End), schedule.End, DbType.Time);
            parameters.Add(nameof(ScheduleWriteModel.ActivityId), schedule.ActivityId, DbType.Guid);
            parameters.Add(nameof(ScheduleWriteModel.DayOfWeek), schedule.DayOfWeek, DbType.Byte);

            return parameters;
        }

        #endregion Private Helpers

    }
}
