using AppRazorWeb.API.ViewModels;
using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using AppRazorWeb.Migrations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppRazorWeb.API.Pages.Schedules
{
    public class AddModel : PageModel
    {

        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();

        [BindProperty]
        public ScheduleViewModel ScheduleViewModel { get; set; }
        public ActivityViewModel activityViewModel { get; set; }
        
        public void OnPost()
        {

            #region Validations

            if (ScheduleViewModel.Start > ScheduleViewModel.End)
            {
                ModelState.AddModelError("ScheduleViewModel.Start", "Start time don't be greater than End time");
            }

            if (ScheduleViewModel.End < ScheduleViewModel.Start)
            {
                ModelState.AddModelError("ScheduleViewModel.End", "End time don't be less than Start time");
            }

            if (ScheduleViewModel.DayOfWeek > 6)
            {
                ModelState.AddModelError("ScheduleViewModel.DayOfWeek", "The day of week don't be greater than 6");
            }

            #endregion Validations

            if (ModelState.IsValid)
            {
                var ScheduleDomainModel = new Schedule
                {
                    Start = ScheduleViewModel.Start,
                    End = ScheduleViewModel.End,
                    DayOfWeek = ScheduleViewModel.DayOfWeek,
                    ActivityId = ScheduleViewModel.ActivityId
                };

                dbContext.Schedule.Add(ScheduleDomainModel);
                dbContext.SaveChanges();
                ViewData["Message"] = "Schedule created successfully!!";
            }
            else
            {
                //Show errors
            }
        }
    }
}


