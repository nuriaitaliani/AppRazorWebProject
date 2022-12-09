using AppRazorWeb.API.ViewModels;
using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace AppRazorWeb.API.Pages.Activities
{
    public class EditModel : PageModel
    {

        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();

        [BindProperty]
        public ActivityViewModel editActivityViewModel { get; set; }

        public void OnGet(Guid id)
        {
            var activity = dbContext.Activity.Find(id);

            if (activity != null)
            {
                editActivityViewModel = new ActivityViewModel()
                {
                    Id = activity.Id,
                    Name = activity.Name,
                    Description = activity.Description,
                    Place = activity.Place,
                    StudentName = activity.StudentName,
                    DailyLesson = activity.DailyLesson,
                    StudentCourse = activity.StudentCouse
                };
            }
        }

        public void OnPostUpdate()
        {

            if (editActivityViewModel != null)
            {
                var existingActivity = dbContext.Activity.Find(editActivityViewModel.Id);
                if (existingActivity != null)
                {
                    existingActivity.Name = editActivityViewModel.Name;
                    existingActivity.Description = editActivityViewModel.Description;
                    existingActivity.Place = editActivityViewModel.Place;
                    existingActivity.StudentName = editActivityViewModel.StudentName;
                    existingActivity.DailyLesson = editActivityViewModel.DailyLesson;
                    existingActivity.StudentCouse = editActivityViewModel.StudentCourse;

                    dbContext.SaveChanges();

                    ViewData["Message"] = "Activity updated succesfully!!";
                }
            }
        }

        public IActionResult OnPostDelete()
        {
            var existingActivity = dbContext.Activity.Find(editActivityViewModel.Id);

            if (existingActivity != null)
            {
                dbContext.Activity.Remove(existingActivity);
                dbContext.SaveChanges();

                return RedirectToPage("/Activities/List");
            }
            return Page();
        }
    }
}
