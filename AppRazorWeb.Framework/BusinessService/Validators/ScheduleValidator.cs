using ResultCommunication;
using System;
using System.Data;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices;

namespace AppRazorWeb.Framework.BusinessService.Validators
{
    public class ScheduleValidator
    {

        public static async Task<IExecutionResult> ValidateSchedule(ScheduleWriteModel schedule,
            IScheduleDataService scheduleDataService, IActivityDataService activityDataService,
            IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {

            if (schedule.Start > schedule.End)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(ScheduleHeader),
                    "Attention - Start time don't be greater than End time");
            }

            if (schedule.DayOfWeek > DayOfWeek.Saturday)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(ScheduleHeader.DayOfWeek),
                    "Attention - The day of week don't be greater than 6");
            }

            if (schedule.DayOfWeek < DayOfWeek.Sunday)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(ScheduleHeader.DayOfWeek),
                    "Attention - The day of week don't be less than 0");
            }

            if (await activityDataService.GetActivity(schedule.ActivityId, dbConnection, dbTransaction) == null)
            {
                return new ExecutionResult(
                    Enums.ErrorType.NotFound,
                    nameof(ActivityHeader),
                    "Attention - The activity doesn't exist");
            }

            if ((await scheduleDataService.GetSchedules(dbConnection, dbTransaction))
                .Exists(q => (q.Start >= schedule.Start && q.End <= schedule.End) &&
                q.DayOfWeek == schedule.DayOfWeek && !q.Id.Equals(schedule.Id) && q.ActivityId.Equals(schedule.ActivityId)))
            {
                return new ExecutionResult(
                    Enums.ErrorType.RelatedRecord,
                    nameof(ScheduleHeader),
                    "Attention - There's already a schedule on that time frame for that activity");
            }

            return new ExecutionResult();

        }

    }
}
