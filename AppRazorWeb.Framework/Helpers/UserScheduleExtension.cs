namespace AppRazorWeb.Framework.Helpers
{
    public static class UserScheduleExtension
    {

        public static BusinessService.Models.UserSchedule_User ToBusinessServiceUser(
            this Dataservices.Models.UserScheduleReadModel userSchedule)
        {
            if (userSchedule == null)
            {
                return null;
            }

            return new BusinessService.Models.UserSchedule_User()
            {
                ScheduleId = userSchedule.ScheduleId,
                ActivityId = userSchedule.ActivityId,
                ActivityName = userSchedule.ActivityName
            };
        } 

        public static BusinessService.Models.UserSchedule_Schedule ToBusinessServiceSchedule(
            this Dataservices.Models.UserScheduleReadModel userSchedule)
        {
            if (userSchedule == null)
            {
                return null;
            }

            return new BusinessService.Models.UserSchedule_Schedule()
            {
                UserId = userSchedule.UserId,
                ActivityId = userSchedule.ActivityId,
                ActivityName = userSchedule.ActivityName,
            };
        }

    }
}
