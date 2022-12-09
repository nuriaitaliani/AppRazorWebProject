namespace AppRazorWeb.Framework.Helpers
{
    public static class ActivityExtensions
    {

        public static BusinessService.Models.ActivityHeader ToBusinessServiceHeaderModel(
            this Dataservices.Models.ActivityReadModel activity)
        {
            if (activity == null)
            {
                return null;
            }

            return new BusinessService.Models.ActivityHeader()
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Place = activity.Place,
                StudentName = activity.StudentName,
                DailyLesson = activity.DailyLesson,
                StudentCourse = activity.StudentCourse
            };
        }

        public static BusinessService.Models.Activity ToBusinessServiceModel(
            this Dataservices.Models.ActivityReadModel activity)
        {
            if (activity == null)
            {
                return null;
            }

            return new BusinessService.Models.Activity()
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Place = activity.Place,
                StudentName = activity.StudentName,
                DailyLesson = activity.DailyLesson,
                StudentCourse = activity.StudentCourse
            };
        }

    }
}