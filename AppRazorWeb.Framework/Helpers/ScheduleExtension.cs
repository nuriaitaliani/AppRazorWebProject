namespace AppRazorWeb.Framework.Helpers
{
    public static class ScheduleExtensions
    {

        public static BusinessService.Models.Schedule ToBusinessServiceModel(
            this Dataservices.Models.ScheduleReadModel schedule)
        {
            if (schedule == null)
            {
                return null;
            }

            return new BusinessService.Models.Schedule()
            {
                Id = schedule.Id,
                Start = schedule.Start,
                End = schedule.End,
                DayOfWeek = schedule.DayOfWeek
            };
        }

        public static BusinessService.Models.ScheduleHeader ToBusinessHeaderModel(
            this Dataservices.Models.ScheduleReadModel schedule)
        {
            if (schedule == null)
            {
                return null;
            }

            return new BusinessService.Models.ScheduleHeader()
            {
                Id = schedule.Id,
                Start = schedule.Start,
                End = schedule.End,
                DayOfWeek = schedule.DayOfWeek
            };
        }

    }
}
