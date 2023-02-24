using System;
using System.Collections.Generic;

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

        public Guid ActivityId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

    }

    public class ScheduleWriteModel : ScheduleHeader
    {
        public List<Guid> Users { get; set; }

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

        public List<UserSchedule_Schedule> UserSchedules { get; set; }
    }

    public class UserSchedule_Schedule
    {
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; }
    }

}
