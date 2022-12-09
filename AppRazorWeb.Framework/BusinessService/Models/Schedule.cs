using System;

namespace AppRazorWeb.Framework.BusinessService.Models
{
    /// <summary>
    /// ScheduleHeader model
    /// </summary>
    public class ScheduleHeader
    {

        public Guid Id { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

    }

    public class ScheduleWriteModel : ScheduleHeader
    {

        public Guid ActivityId { get; set; }

        public Dataservices.Models.ScheduleWriteModel ToDataServiceModel()
        {
            return new Dataservices.Models.ScheduleWriteModel()
            {
                Id = Id,
                Start = Start,
                End = End,
                ActivityId = ActivityId,
                DayOfWeek = DayOfWeek
            };
        }

    }

    public class Schedule : ScheduleHeader
    {

        public ActivityHeader Activity { get; set; }

    }

}
