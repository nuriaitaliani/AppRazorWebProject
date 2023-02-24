using ResultCommunication;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AppRazorWeb.Framework.BusinessService.Models;
using AppRazorWeb.Framework.Dataservices;

namespace AppRazorWeb.Framework.BusinessService.Validators
{
    public class ActivityValidator
    {

        public static async Task<IExecutionResult> ValidateActivity(ActivityHeader activity,
            IActivityDataService activityDataService,
            IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {

            #region Mandatory Fields


            if (string.IsNullOrWhiteSpace(activity.Name))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(ActivityHeader.Name),
                    "Attention - The name is mandatory");
            }

            if (string.IsNullOrWhiteSpace(activity.Place))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(ActivityHeader.Place),
                    "Attention - The place is mandatory");
            }

            if (string.IsNullOrWhiteSpace(activity.StudentName))
            {
                return new ExecutionResult(
                    Enums.ErrorType.MandatoryField,
                    nameof(ActivityHeader.StudentName),
                    "Attention - The student name is mandatory");
            }

            #endregion Mandatory Fields


            #region Length

            if (activity.Name.Length > 50)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.Name),
                    "Attention - The name can't have more than 50 characters");
            }

            if (activity.Description.Length > 140)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.Description),
                    "Attention - The description can't have more than 50 characters");
            }

            if (activity.Place.Length > 50)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader),
                    "Attention - The place can't have more than 50 characters");
            }

            if (activity.StudentName.Length > 50)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.StudentName),
                    "Attention - The student name can't have more than 50 characters");
            }

            if (activity.DailyLesson.Length > 50)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.DailyLesson),
                    "Attention - The daily lesson can't have more than 50 characters");
            }

            if (activity.StudentCourse > 12)
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.StudentCourse),
                    "Attention - The student course don't be greater than 12");
            }

            if (activity.StudentCourse < 0 )
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.StudentCourse),
                    "Attention - The student course don't be less than 0");
            }

            #endregion Length


            string patternIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
            Regex r = new Regex(patternIsLetter);
            if (!r.IsMatch(activity.Name))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.Name),
                    "Attention - The name only have to contain letters");
            }

            //if ((await activityDataService.GetActivities(dbConnection, dbTransaction))     
            //    .Exists(q => q.Name.Equals(activity.Name) &&
            //    !q.Id.Equals(activity.Id)))
            //{
            //    return new ExecutionResult(
            //        Enums.ErrorType.RepeatedRecord,
            //        "Attention - The name already exist");
            //}

            if (!r.IsMatch(activity.Place))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.Place),
                    "Attention - The place only have to contain letters");
            }

            if (!r.IsMatch(activity.StudentName))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.StudentName),
                    "Attention - The name only have to contain letters");
            }

            if (!r.IsMatch(activity.DailyLesson))
            {
                return new ExecutionResult(
                    Enums.ErrorType.InvalidField,
                    nameof(ActivityHeader.DailyLesson),
                    "Attention - The daily lesson only have to contain letters");
            }

            int num = activity.StudentCourse;

            return new ExecutionResult();

        }

        public static async Task<IExecutionResult> ValidateActivityOnDelete(Guid activityId,
            IScheduleDataService scheduleDataService, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            //Devuelves todos los schedules filtrados por su Id
            if ((await scheduleDataService.GetSchedules(dbConnection, dbTransaction, new Dataservices.Filters.ScheduleFilters()
            {
                //Si el nº de ID's de activities es distinto de 0 entonces es que existen schedules asociados 
                ActivityId = activityId
            })).Count != 0)
            {
                //Y devolvemos un error
                return new ExecutionResult(
                    Enums.ErrorType.RelatedRecord,
                    nameof(ScheduleHeader),
                    "Attention - You can't delete an activity if it has an associated schedule");
            }

            return new ExecutionResult();
        }

    }
}

