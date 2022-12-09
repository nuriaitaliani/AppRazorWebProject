using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using AppRazorWeb.Migrations.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AppRazorWeb.API.Pages.Activities
{
    public class ListModel : PageModel
    {

        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();
        public List<Activity> activities { get; set; }


        public void OnGet()
        {
            activities = dbContext.Activity.ToList();
        }
    }
}
