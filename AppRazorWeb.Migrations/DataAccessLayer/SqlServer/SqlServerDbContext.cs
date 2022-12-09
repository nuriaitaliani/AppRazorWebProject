using Microsoft.EntityFrameworkCore;
using AppRazorWeb.Migrations.Helper;

namespace AppRazorWeb.Migrations.DataAccessLayer.SqlServer
{
    public class SqlServerDbContext : DbContextHelper
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Common.Activities.GetTableDefinitions(builder);
            Common.Schedules.GetTableDefinitions(builder);
            Common.Users.GetTableDefinitions(builder);
            Common.UserSchedule.GetTableDefinitions(builder);        
        }
    }

    public class DevelopmentSqlServerDbContext : SqlServerDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            System.IO.Directory.GetCurrentDirectory();

            options.UseSqlServer("Server = localhost,1433; Database = test; User Id=sa; Password=qa1ws2ed34;",
                e => e.MigrationsHistoryTable("apprazorweb_migration"));
        }
    }

}
