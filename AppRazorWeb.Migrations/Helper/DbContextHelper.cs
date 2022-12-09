using Microsoft.EntityFrameworkCore;

namespace AppRazorWeb.Migrations.Helper
{
    /// <summary>
    /// Database context helper
    /// </summary>
    public class DbContextHelper : DbContext
    {

        /// <summary>
        /// Activities table
        /// </summary>
        public DbSet<Models.Activity> Activity { get; set; }

        /// <summary>
        /// Schedules table
        /// </summary>
        public DbSet<Models.Schedule> Schedule { get; set; }

        /// <summary>
        /// Users table
        /// </summary>
        public DbSet<Models.User> User { get; set; }

        /// <summary>
        /// UserSchedule table
        /// </summary>
        public DbSet<Models.UserSchedule> UserSchedule { get; set; }

    }
}
