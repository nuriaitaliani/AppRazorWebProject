using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using AppRazorWeb.Migrations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AppRazorWeb.API.Pages.Users
{
    public class ListModel : PageModel
    {
        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();
        public List<User> users { get; set; }


        public void OnGet()
        {
            users = dbContext.User.ToList();
        }
    }
}
