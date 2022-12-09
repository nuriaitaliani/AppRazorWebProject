using System;
using System.Collections.Generic;

namespace AppRazorWeb.Framework.BusinessService.Models
{
    /// <summary>
    /// ActivityHeader model
    /// </summary>
    public class ActivityHeader
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string StudentName { get; set; }

        public string DailyLesson { get; set; }

        public int StudentCourse { get; set; }

    }

    public class ActivityClassWriteModel : ActivityHeader
    {

        public Dataservices.Models.ActivityWriteModel ToDataServiceModel()
        {
            return new Dataservices.Models.ActivityWriteModel()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Place = Place,
                StudentName = StudentName,
                DailyLesson = DailyLesson,
                StudentCourse = StudentCourse
            };
        }
    }

    public class Activity : ActivityHeader
    {
        public List<ScheduleHeader> Schedules { get; set; }
    }

}
