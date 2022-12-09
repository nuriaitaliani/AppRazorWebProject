using Microsoft.EntityFrameworkCore;

namespace AppRazorWeb.Migrations.DataAccessLayer.Common
{
    public class UserSchedule : DbContext
    {

        public static void GetTableDefinitions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.UserSchedule>()
                .HasOne(e => e.Schedule)
                .WithMany(e => e.UserScheduleRelation)
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Schedules_UserSchedule");

            modelBuilder.Entity<Models.UserSchedule>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserScheduleRelation)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Users_UserSchedule");
        }

    }
}
