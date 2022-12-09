using AppRazorWeb.API.ViewModels;
using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using AppRazorWeb.Migrations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace AppRazorWeb.API.Pages.Activities
{
    public class AddModel : PageModel
    {
        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();

        [BindProperty]
        public ActivityViewModel ActivityViewModelRequest { get; set; }
                        
        public void OnPost()
        {

            #region Validations

            #region Name

            if (string.IsNullOrWhiteSpace(ActivityViewModelRequest.Name))
            {
                ModelState.AddModelError("ActivityViewModelRequest.Name", "The name is mandatory");
            }
            else
            {
                if (ActivityViewModelRequest.Name.Length > 50)
                {
                    ModelState.AddModelError("ActivityViewModelRequest.Name", "The name can't have more than 50 characters");
                }

                string patternIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
                Regex r = new Regex(patternIsLetter);
                if (!r.IsMatch(ActivityViewModelRequest.Name))
                {
                    ModelState.AddModelError("ActivityViewModelRequest.Name", "The name only have to contain letters");
                }
            }

            #endregion Name

            #region Description

            if (!string.IsNullOrWhiteSpace(ActivityViewModelRequest.Description))
            {
                if (ActivityViewModelRequest.Description.Length > 50)
                {
                    ModelState.AddModelError("ActivityViewModelRequest.Description", "The name can't have more than 50 characters");
                }
            }

            #endregion Description

            #region Place

            if (string.IsNullOrWhiteSpace(ActivityViewModelRequest.Place))
            {
                ModelState.AddModelError("ActivityViewModelRequest.Place", "The place is mandatory");
            }
            else
            {
                if (ActivityViewModelRequest.Place.Length > 50)
                {
                    ModelState.AddModelError("ActivityViewModelRequest.Place", "The place can't have more than 50 characters");
                }
                string patternIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
                Regex r = new Regex(patternIsLetter);
                if (!r.IsMatch(ActivityViewModelRequest.Place))
                {
                    ModelState.AddModelError("ActivityViewModelRequest.Place", "The place only have to contain letters");
                }
            }

            #endregion Place

            #region StudentName

            if (string.IsNullOrWhiteSpace(ActivityViewModelRequest.StudentName))
            {
                ModelState.AddModelError("ActivityViewModelRequest.StudentName", "The student name is mandatory");
            }
            else
            {
                if (ActivityViewModelRequest.StudentName.Length > 50)
                {
                    ModelState.AddModelError("ActivityViewModelRequest.StudentName", "The student name can't have more than 50 characters");
                }
                string patternIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
                Regex r = new Regex(patternIsLetter);
                if (!r.IsMatch(ActivityViewModelRequest.StudentName))
                {
                    ModelState.AddModelError("ActivityViewModelRequest.StudentName", "The student name only have to contain letters");
                }
            }

            #endregion StudentName

            #region DailyLesson

            if (!string.IsNullOrWhiteSpace(ActivityViewModelRequest.DailyLesson))
            {
                if (ActivityViewModelRequest.DailyLesson.Length > 50)
                {
                    ModelState.AddModelError("ActivityViewModelRequest.DailyLesson", "The daily lesson can't have more than 50 characters");
                }
                string patternIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
                Regex r = new Regex(patternIsLetter);
                if (!r.IsMatch(ActivityViewModelRequest.DailyLesson))
                {
                    ModelState.AddModelError("ActivityViewModelRequest.DailyLesson", "The daily lesson only have to contain letters");
                }
            }

            #endregion DailyLesson

            #region StudentCourse

            if (ActivityViewModelRequest.StudentCourse > 12)
            {
                ModelState.AddModelError("ActivityViewModelRequest.StudentCourse","The student course don't be greater than 12");
            }
            if (ActivityViewModelRequest.StudentCourse < 0)
            {
                ModelState.AddModelError("ActivityViewModelRequest.StudentCourse","The student course don't be less than 0");
            }


            #endregion StudentCourse

            #endregion Validations

            if (ModelState.IsValid)
            {
                var ActivityDomainModel = new Activity
                {
                    Name = ActivityViewModelRequest.Name,
                    Description = ActivityViewModelRequest.Description,
                    Place = ActivityViewModelRequest.Place,
                    StudentName = ActivityViewModelRequest.StudentName,
                    DailyLesson = ActivityViewModelRequest.DailyLesson,
                    StudentCouse = ActivityViewModelRequest.StudentCourse
                };

                dbContext.Activity.Add(ActivityDomainModel);
                dbContext.SaveChanges();
                ViewData["Message"] = "Activity created successfully!!";
            }
            else
            {
                //Show errors
            }
        }
    }
}