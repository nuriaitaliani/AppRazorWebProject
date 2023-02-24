using ResultCommunication;
using System;
using System.Data;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices;
using System.Numerics;

namespace AppRazorWeb.Framework.BusinessService.Validators
{
    public class ScheduleValidator
    {

        public static async Task<IExecutionResult> ValidateSchedule(ScheduleWriteModel schedule,
            IScheduleDataService scheduleDataService, IActivityDataService activityDataService,
            IUserDataService userDataService, IUserScheduleDataService userScheduleDataService,
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

            if (schedule.Users != null &&
                schedule.Users.Count != 0)
            {
                foreach (Guid userId in schedule.Users)
                {
                    if (await userDataService.GetUser(userId, dbConnection, dbTransaction)==null)
                    {
                        return new ExecutionResult(
                            Enums.ErrorType.NotFound,
                            nameof(Schedule),
                            "Attention - The user doesn't exist");
                    }
                }
            }

            if (schedule.Users != null &&
                schedule.Users.Count != 0)
            {

                foreach (Guid userId in schedule.Users)
                {
                    if ((await userScheduleDataService.GetUserSchedules(dbConnection, dbTransaction, new Dataservices.Filters.UserScheduleFilters()
                    {
                        UserId = userId
                    })).Exists(q => (schedule.Start.Equals(q.ScheduleStart) && schedule.End.Equals(q.ScheduleEnd)) || //14 - 18 (Repetición)
                        (schedule.Start >= q.ScheduleStart && schedule.End <= q.ScheduleEnd) || //15 > 14, 17 < 18 (Interior, anterior +1, posterior -1)
                        (schedule.Start <= q.ScheduleStart && schedule.End >= q.ScheduleEnd) && //13 < 14, 19 > 18 (Anterior - 1, posterior +1, rodeando)
                        schedule.DayOfWeek.Equals(q.ScheduleDayOfWeek) &&
                        !q.ScheduleId.Equals(schedule.Id)))
                    {
                        return new ExecutionResult(
                            Enums.ErrorType.RelatedRecord,
                            nameof(Schedule),
                            "Attention - There's already a schedule on that time frame for that user");
                    }
                }
            }


            return new ExecutionResult();

        }

    }
}
