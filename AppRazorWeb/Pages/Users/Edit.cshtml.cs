using AppRazorWeb.API.ViewModels;
using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace AppRazorWeb.API.Pages.Users
{
    public class EditModel : PageModel
    {
        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();

        [BindProperty]
        public UserViewModel userViewModel { get; set; }

        public void OnGet(Guid id)
        {
            var user = dbContext.User.Find(id);

            if (user != null)
            {
                userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    DNI = user.DNI,
                    Age = user.Age,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
            }
        }

        public void OnPostUpdate()
        {

            if (userViewModel != null)
            {
                var existingUser = dbContext.User.Find(userViewModel.Id);
                if (existingUser != null)
                {
                    existingUser.Name = userViewModel.Name;
                    existingUser.LastName = userViewModel.LastName;
                    existingUser.DNI = userViewModel.DNI;
                    existingUser.Age = userViewModel.Age;
                    existingUser.PhoneNumber = userViewModel.PhoneNumber;
                    existingUser.Email = userViewModel.Email;

                    dbContext.SaveChanges();

                    ViewData["Message"] = "User updated succesfully!!";
                }
            }
        }

        public IActionResult OnPostDelete()
        {
            var existingUser = dbContext.User.Find(userViewModel.Id);

            if (existingUser != null)
            {
                dbContext.User.Remove(existingUser);
                dbContext.SaveChanges();

                return RedirectToPage("/Users/List");
            }
            return Page();
        }

    }
}
