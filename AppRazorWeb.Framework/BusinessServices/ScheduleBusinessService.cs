using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.BusinessService.Validators;
using AppRazorWeb.Framework.Dataservices;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Helpers;
using Microsoft.Data.SqlClient;
using ResultCommunication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppRazorWeb.Framework.BusinessService
{
    public class ScheduleBusinessService : IScheduleBusinessService
    {

        #region Fields

        private readonly IScheduleDataService _scheduleDataService;
        private readonly IActivityDataService _activityDataService;
        private readonly IUserScheduleDataService _userScheduleDataService;
        private readonly IUserDataService _userDataService;
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        #endregion Fields

        #region Constructor

        public ScheduleBusinessService(IScheduleDataService scheduleDataService,
            IActivityDataService activityDataService,
            IUserScheduleDataService userScheduleDataService,
            IUserDataService userDataService,
            string connectionString)
        {
            _scheduleDataService = scheduleDataService;
            _activityDataService = activityDataService;
            _userScheduleDataService = userScheduleDataService;
            _userDataService = userDataService;
            _connection = new SqlConnection(connectionString);
        }

        #endregion Constructor

        #region IScheduleBusinessService implementation

        #region Creates

        public async Task<IExecutionResult> CreateSchedule(ScheduleWriteModel schedule)
        {
            try
            {
                _connection.Open();
                _connection.BeginTransaction();

                IExecutionResult result = await ScheduleValidator.ValidateSchedule(schedule, _scheduleDataService,
                    _activityDataService, _userDataService, _userScheduleDataService, _connection, _transaction);

                if (!result.Success)
                {
                    return result;
                }

                if (schedule.Id.Equals(Guid.Empty))
                {
                    schedule.Id = Guid.NewGuid();
                }

                await _scheduleDataService.InsertSchedule(schedule.ToDataServiceModel(), _connection, _transaction);

                if (schedule.Users != null &&
                    schedule.Users.Count != 0)
                {
                    foreach (Guid userId in schedule.Users)
                    {
                        await _userScheduleDataService.InsertUserSchedule(new Dataservices.Models.UserScheduleReadModel()
                        {
                            UserId = userId,
                            ScheduleId = schedule.Id
                        }, _connection, _transaction);
                    }
                }

                _transaction.Commit();

                return new ExecutionResult();

            }
            catch (Exception exception)
            {
                _transaction.Rollback();

                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion Creates

        #region Deletes

        public async Task<IExecutionResult> DeleteSchedule(Guid scheduleId)
        {
            try
            {
                _connection.Open();
                _connection.BeginTransaction();

                if (await _scheduleDataService.GetSchedule(scheduleId, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(ScheduleHeader),
                        "Attention - The schedule doesn't exist");
                }

                await _userScheduleDataService.DeleteUserScheduleByScheduleId(scheduleId, _connection, _transaction);
                await _scheduleDataService.DeleteSchedule(scheduleId, _connection, _transaction);

                _transaction.Commit();

                return new ExecutionResult();

            }
            catch (Exception exception)
            {
                _transaction.Rollback();

                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion Deletes

        #region Gets

        public async Task<IExecutionResult> GetSchedule(Guid scheduleId)
        {
            try
            {
                _connection.Open();

                IExecutionResult result = await ScheduleHelper.GetSchedule(scheduleId, _scheduleDataService, _activityDataService, _userScheduleDataService, _connection);

                if (!result.Success)
                {
                    return result;
                }

                return new ExecutionResult(result.Result);

            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<IExecutionResult> GetSchedules(ScheduleFilters filters)
        {
            try
            {
                _connection.Open();

                List<ScheduleHeader> schedules = (await _scheduleDataService.GetSchedules(_connection, filters: filters))
                    .Select(schedule => schedule.ToBusinessHeaderModel()).ToList();

                return new ExecutionResult(schedules);
            }
            catch (Exception exception)
            {
                return new ExecutionResult(
                   Enums.ErrorType.GeneralException,
                   exception.GetType().ToString(),
                   exception.Message);
            }
            finally
            {
                _connection.Close();
            }

        }

        #endregion Gets

        #region Updates

        public async Task<IExecutionResult> UpdateSchedule(ScheduleWriteModel schedule)
        {
            try
            {
                _connection.Open();
                _connection.BeginTransaction();

                if (await _scheduleDataService.GetSchedule(schedule.Id, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(ScheduleHeader),
                        "Attention - The schedule doesn't exist");
                }

                IExecutionResult result = await ScheduleValidator.ValidateSchedule(schedule, _scheduleDataService,
                    _activityDataService, _userDataService, _userScheduleDataService, _connection, _transaction);

                if (!result.Success)
                {
                    return result;
                }

                await _scheduleDataService.UpdateSchedule(schedule.ToDataServiceModel(), _connection, _transaction);

                if (schedule.Users !=null &&
                    schedule.Users.Count != 0)
                {
                    foreach (Guid userId in schedule.Users)
                    {
                        await _userScheduleDataService.DeleteUserScheduleByScheduleId(schedule.Id, _connection, _transaction);

                        if (schedule.Users.Contains(userId))
                        {
                            await _userScheduleDataService.InsertUserSchedule(new Dataservices.Models.UserScheduleWriteModel()
                            {
                                UserId = userId,
                                ScheduleId = schedule.Id,
                            }, _connection, _transaction);
                        }
                    }
                }

                _transaction.Commit();

                return new ExecutionResult();

            }
            catch (Exception exception)
            {
                _transaction.Rollback();

                return new ExecutionResult(
                    Enums.ErrorType.GeneralException,
                    exception.GetType().ToString(),
                    exception.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion Updates

        #endregion IScheduleBusinessService implementation

    }
}
