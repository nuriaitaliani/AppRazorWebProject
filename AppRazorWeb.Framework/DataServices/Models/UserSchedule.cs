using System;

namespace AppRazorWeb.Framework.Dataservices.Models
{
    public class UserScheduleWriteModel
    {

        public Guid UserId { get; set; }

        public Guid ScheduleId { get; set; }

    }

    public class UserScheduleReadModel : UserScheduleWriteModel
    {
        public string UserName { get; set; }

        public Guid ActivityId { get; set; }

        public string ActivityName { get; set; }

        public TimeSpan ScheduleStart { get; set; }

        public TimeSpan ScheduleEnd { get; set; }

        public DayOfWeek ScheduleDayOfWeek { get; set; }
    }

}
