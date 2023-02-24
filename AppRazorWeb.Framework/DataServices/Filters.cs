using System;

namespace AppRazorWeb.Framework.Dataservices.Filters
{
    public class ActivityFilters
    {

        public string Name { get; set; }

        public string Place { get; set; }

        public string StudentName { get; set; }

        public string DailyLesson { get; set; }

        public int? StudentCourse { get; set; }

    }

    public class UserFilters
    {

        public string Name { get; set; }

        public string LastName { get; set; }

        public string DNI { get; set; }

        public int? Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

    }

    public class ScheduleFilters
    {

        public TimeSpan? Start { get; set; }

        public TimeSpan? End { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public Guid? ActivityId { get; set; }

    }


    public class UserScheduleFilters
    {

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public Guid? ScheduleId { get; set; }

        public Guid? ActivityId { get; set; }

        public string ActivityName { get; set; }

    }

}
