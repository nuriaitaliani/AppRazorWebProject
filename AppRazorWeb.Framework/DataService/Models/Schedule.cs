using System;

namespace AppRazorWeb.Framework.Dataservices.Models
{
    public class ScheduleWriteModel
    {

        public Guid Id { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public Guid ActivityId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

    }

    public class ScheduleReadModel : ScheduleWriteModel
    {
        public string ActivityName { get; set; }

    }
}
