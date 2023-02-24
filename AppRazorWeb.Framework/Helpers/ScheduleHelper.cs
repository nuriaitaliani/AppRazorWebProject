using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices;
using ResultCommunication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppRazorWeb.Framework.Helpers
{
    public static class ScheduleHelper
    {

        public static async Task<IExecutionResult> GetSchedule(Guid scheduleId,
            IScheduleDataService scheduleDataService, 
            IActivityDataService activityDataService, 
            IUserScheduleDataService userScheduleDataService,
            IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            Schedule schedule = (await scheduleDataService.GetSchedule(scheduleId, dbConnection,
                dbTransaction)).ToBusinessServiceModel();

            if (schedule == null)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(schedule),
                    "Attention - The schedule doesn't exist");
            }

            List<Dataservices.Models.UserScheduleReadModel> userSchedules = await userScheduleDataService
                .GetUserSchedules(dbConnection, dbTransaction, new Dataservices.Filters.UserScheduleFilters()
                {
                    ScheduleId = scheduleId,
                });

            Dataservices.Models.ActivityReadModel activityReadModel = await activityDataService
                .GetActivity(schedule.ActivityId, dbConnection, dbTransaction);

            schedule.UserSchedules = userSchedules
                .Select(q => q.ToBusinessServiceSchedule())
                .ToList();

            return new ExecutionResult(schedule);
        }

    }
}
