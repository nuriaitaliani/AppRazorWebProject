using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using AppRazorWeb.Migrations.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AppRazorWeb.API.Pages.Schedules
{
    public class ListModel : PageModel
    {
        DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();
        public List<Schedule> schedules { get; set; }


        public void OnGet()
        {
            schedules = dbContext.Schedule.ToList();
        }
    }
}
