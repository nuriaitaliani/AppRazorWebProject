using Microsoft.Data.SqlClient;
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
    public class ActivityBusinessService : IActivityBusinessService
    {

        #region Fields

        private readonly IActivityDataService _activityDataService;
        private readonly IScheduleDataService _scheduleDataService;
        private readonly SqlConnection _connection;

        #endregion Fields

        #region Constructor

        public ActivityBusinessService(IActivityDataService activityDataService, IScheduleDataService scheduleDataService,
            string connectionString)
        {
            _activityDataService = activityDataService;
            _connection = new SqlConnection(connectionString);
            _scheduleDataService = scheduleDataService;
        }

        #endregion Constructor

        #region IActivityBusinessService implementation

        #region Creates

        public async Task<IExecutionResult> CreateActivity(ActivityClassWriteModel activity)
        {
            try
            {
                _connection.Open();

                IExecutionResult result = await ActivityValidator.ValidateActivity(activity, _activityDataService, _connection);
                if (!result.Success)
                {
                    return result;
                }

                if (activity.Id.Equals(Guid.Empty))
                {
                    activity.Id = Guid.NewGuid();
                }

                await _activityDataService.InsertActivity(activity.ToDataServiceModel(), _connection);

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

        public async Task<IExecutionResult> DeleteActivity(Guid activityId)
        {
            try
            {
                _connection.Open();

                if (await _activityDataService.GetActivity(activityId, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(ActivityHeader),
                        "Attention - The activity doesn't exist");
                }

                //Comprobamos si una actividad está asociada a un horario antes de eliminarla
                IExecutionResult result = await ActivityValidator.ValidateActivityOnDelete(activityId, _scheduleDataService, _connection);

                if (!result.Success)
                {
                    return result;
                }

                await _activityDataService.DeleteActivity(activityId, _connection);

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

        public async Task<IExecutionResult> GetActivity(Guid activityId)
        {
            try
            {
                _connection.Open();

                Activity activity = (await _activityDataService
                    .GetActivity(activityId, _connection))
                    .ToBusinessServiceModel();

                if (activity == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(Activity),
                        "Attention - The activity doesn't exist");
                }

                activity.Schedules = (await _scheduleDataService
                    .GetSchedules(_connection, filters: new ScheduleFilters()
                    {
                        ActivityId = activityId
                    })).Select(q => q.ToBusinessHeaderModel())
                    .ToList();

                return new ExecutionResult(activity);
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

        public async Task<IExecutionResult> GetActivities(ActivityFilters activityFilters)
        {
            try
            {
                _connection.Open();

                List<ActivityHeader> activities = (await _activityDataService.GetActivities(_connection, filters: activityFilters))
                    .Select(activities => activities.ToBusinessServiceHeaderModel()).ToList();

                return new ExecutionResult(activities);
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

        public async Task<IExecutionResult> UpdateActivity(ActivityClassWriteModel activity)
        {
            try
            {
                _connection.Open();

                if (await _activityDataService.GetActivity(activity.Id, _connection) == null)
                {
                    return new ExecutionResult(
                        Enums.ErrorType.NotFound,
                        nameof(Activity),
                        "Attention - The discipline doesn't exist");
                }

                IExecutionResult result = await ActivityValidator.ValidateActivity(activity, _activityDataService, _connection);
                if (!result.Success)
                {
                    return result;
                }

                await _activityDataService.UpdateActivity(activity.ToDataServiceModel(), _connection);

                return new ExecutionResult(result);
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

        #endregion IActivityBusinessService implementation

    }
}
