using System;

namespace AppRazorWeb.API.ViewModels
{
    public class ScheduleViewModel
    {
        public Guid Id { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public byte DayOfWeek { get; set; }

        public Guid ActivityId { get; set; }
    }
}
