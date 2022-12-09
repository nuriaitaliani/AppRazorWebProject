using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ResultCommunication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.BusinessService.Validators;
using AppRazorWeb.Framework.Dataservices;
using AppRazorWeb.Framework.Dataservices.Filters;
using AppRazorWeb.Framework.Helpers;

namespace AppRazorWeb.Framework.BusinessService
{
    public class ScheduleBusinessService : IScheduleBusinessService
    {

        #region Fields

        private readonly IScheduleDataService _scheduleDataService;
        private readonly IActivityDataService _activityDataService;
        private readonly SqlConnection _connection;

        #endregion Fields

        #region Constructor

        public ScheduleBusinessService(IScheduleDataService scheduleDataService, IActivityDataService activityDataService,
            string connectionString)
        {
            _scheduleDataService = scheduleDataService;
            _activityDataService = activityDataService;
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

                IExecutionResult result = await ScheduleValidator.ValidateSchedule(schedule, _scheduleDataService,
                    _activityDataService, _connection);
                if (!result.Success)
                {
                    return result;
                }

                if (schedule.Id.Equals(Guid.Empty))
                {
                    schedule.Id = Guid.NewGuid();
                }

                await _scheduleDataService.InsertSchedule(schedule.ToDataServiceModel(), _connection);

                return new ExecutionResult();

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

        #endregion Creates

        #region Deletes

        public async Task<IExecutionResult> DeleteSchedule(Guid scheduleId)
        {
            try
            {
                _connection.Open();

                if (await _scheduleDataService.GetSchedule(scheduleId, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(ScheduleHeader),
                        "Attention - The schedule doesn't exist");
                }

                await _scheduleDataService.DeleteSchedule(scheduleId, _connection);

                return new ExecutionResult();

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

        #endregion Deletes

        #region Gets

        public async Task<IExecutionResult> GetSchedule(Guid scheduleId)
        {
            try
            {
                _connection.Open();

                Dataservices.Models.ScheduleReadModel dataServiceSchedule = await _scheduleDataService.GetSchedule(scheduleId, _connection);

                if (dataServiceSchedule == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(ScheduleHeader),
                        "Attention - The schedule doesn't exist");
                }

                Schedule schedule = dataServiceSchedule.ToBusinessServiceModel();

                schedule.Activity = (await _activityDataService
                    .GetActivity(dataServiceSchedule.ActivityId, _connection))
                    .ToBusinessServiceHeaderModel();

                return new ExecutionResult(schedule);

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

                if (await _scheduleDataService.GetSchedule(schedule.Id, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(ScheduleHeader),
                        "Attention - The schedule doesn't exist");
                }

                IExecutionResult result = await ScheduleValidator.ValidateSchedule(schedule, _scheduleDataService,
                    _activityDataService, _connection);
                if (!result.Success)
                {
                    return result;
                }

                await _scheduleDataService.UpdateSchedule(schedule.ToDataServiceModel(), _connection);

                return new ExecutionResult();

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

        #endregion Updates

        #endregion IScheduleBusinessService implementation

    }
}
