using AppRazorWeb.Framework.Dataservices;
using AppRazorWeb.Framework.BusinessService.Models;
using ResultCommunication;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AppRazorWeb.Framework.Helpers
{
    public static class ActivityHelper
    {

        public static async Task<IExecutionResult> GetActivity(Guid activityId,
            IActivityDataService activityDataService,
            IScheduleDataService scheduleDataService,
            IDbConnection dbConnection,
            IDbTransaction dbTransaction)
        {
            Activity activity = (await activityDataService.GetActivity(activityId,
                dbConnection, dbTransaction)).ToBusinessServiceModel();

            if (activity == null)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(activity),
                    "Attention - The activity doesn't exist");
            }

            List<Dataservices.Models.ScheduleReadModel> schedules = await scheduleDataService
                .GetSchedules(dbConnection, dbTransaction, new Dataservices.Filters.ScheduleFilters()
                {
                    ActivityId = activityId,
                });

            activity.Schedules = schedules
                .Select(q => q.ToBusinessHeaderModel())
                .ToList();

            return new ExecutionResult(activity);
        }

    }
}
