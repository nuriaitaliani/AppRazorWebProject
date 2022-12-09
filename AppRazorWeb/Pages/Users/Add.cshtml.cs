using AppRazorWeb.API.ViewModels;
using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using AppRazorWeb.Migrations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace AppRazorWeb.API.Pages.Users
{
    public class AddModel : PageModel
    {

        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();

        [BindProperty]
        public UserViewModel UserViewModelRequest { get; set; }

        public void OnPost()
        {

            #region Validators

            #region Name

            if (string.IsNullOrWhiteSpace(UserViewModelRequest.Name))
            {
                ModelState.AddModelError("UserViewModelRequest.Name", "The name is mandatory");
            }
            else
            {
                if (UserViewModelRequest.Name.Length > 50)
                {
                    ModelState.AddModelError("UserViewModelRequest.Name", "The name can't have more than 50 characters");
                }
                string patternNameIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
                Regex r = new Regex(patternNameIsLetter);
                if (!r.IsMatch(UserViewModelRequest.Name))
                {
                    ModelState.AddModelError("UserViewModelRequest.Name", "The name only have to contain letters");
                }
            }

            #endregion Name

            #region LastName

            if (string.IsNullOrWhiteSpace(UserViewModelRequest.LastName))
            {
                ModelState.AddModelError("UserViewModelRequest.LastName", "The last name is mandatory");
            }
            else
            {
                if (UserViewModelRequest.LastName.Length > 50)
                {
                    ModelState.AddModelError("UserViewModelRequest.LastName", "The last name can't have more than 50 characters");
                }
                string patternLastNameIsLetter = @"^([a-zA-ZùÙüÜäàáëèéïìíöòóüùúÄÀÁËÈÉÏÌÍÖÒÓÜÚñÑ\s]+)$";
                Regex r = new Regex(patternLastNameIsLetter);
                if (!r.IsMatch(UserViewModelRequest.LastName))
                {
                    ModelState.AddModelError("UserViewModelRequest.LastName", "The last name only have to contain letters");
                }
            }

            #endregion LastName

            #region DNI

            if (string.IsNullOrWhiteSpace(UserViewModelRequest.DNI))
            {
                ModelState.AddModelError("UserViewModelRequest.DNI", "The dni is mandatory");
            }
            else
            {
                string patternDni = @"^(\d{8}([A-Za-z]))$";
                Regex r = new Regex(patternDni);

                if (!r.IsMatch(UserViewModelRequest.DNI))
                {
                    ModelState.AddModelError("UserViewModelRequest.DNI", "The DNI can't be different than 8 digits and a letter");
                }
            }

            #endregion DNI

            #region Age

            if (UserViewModelRequest.Age > 12)
            {
                ModelState.AddModelError("UserViewModelRequest.Age", "The age don't be greater than 12");
            }
            if (UserViewModelRequest.Age < 0)
            {
                ModelState.AddModelError("UserViewModelRequest.Age", "The age don't be less than 0");
            }

            #endregion Age

            #region PhoneNumber

            if (string.IsNullOrWhiteSpace(UserViewModelRequest.PhoneNumber))
            {
                ModelState.AddModelError("UserViewModelRequest.PhoneNumber", "The phone number is mandatory");
            }
            else
            {
                string patternTelefono = @"^(\+[\/.()-]*){0,3}?(\d[\/.()-]*){9,12}$";
                Regex r = new Regex(patternTelefono);
                if (!r.IsMatch(UserViewModelRequest.PhoneNumber))
                {
                    ModelState.AddModelError("UserViewModelRequest.PhoneNumber", "That's an incorrect format");
                }
            }

            #endregion PhoneNumber

            #region Email

            if (string.IsNullOrWhiteSpace(UserViewModelRequest.Email))
            {
                ModelState.AddModelError("UserViewModelRequest.Email", "The email is mandatory");
            }
            else
            {
                string patternEmail = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}";
                Regex r = new Regex(patternEmail);
                if (!r.IsMatch(UserViewModelRequest.Email))
                {
                    ModelState.AddModelError("UserViewModelRequest.Email", "That's an incorrect format");
                }
            }

            #endregion Email

            #endregion Validators

            if (ModelState.IsValid)
            {
                var UserDomainModel = new User
                {
                    Name = UserViewModelRequest.Name,
                    LastName = UserViewModelRequest.LastName,
                    DNI = UserViewModelRequest.DNI,
                    Age = UserViewModelRequest.Age,
                    PhoneNumber = UserViewModelRequest.PhoneNumber,
                    Email = UserViewModelRequest.Email
                };

                dbContext.User.Add(UserDomainModel);
                dbContext.SaveChanges();
                ViewData["Message"] = "User created successfully!!";
            }
            else
            {
                //Show errors
            }
        }
    }
}
