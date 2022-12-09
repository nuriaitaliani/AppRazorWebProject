using System;

namespace AppRazorWeb.API.ViewModels
{
	public class ActivityViewModel
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string StudentName { get; set; }

        public string DailyLesson { get; set; }

        public int StudentCourse { get; set; }
    }
}
