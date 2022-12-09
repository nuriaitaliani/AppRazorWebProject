using Microsoft.EntityFrameworkCore;

namespace AppRazorWeb.Migrations.DataAccessLayer.Common
{
    public class Schedules : DbContext
    {

        public static void GetTableDefinitions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Schedule>(entity =>
            {
                entity.Property(b => b.Start)
                .IsRequired();
                entity.Property(b => b.End)
                .IsRequired();
                entity.Property(b => b.DayOfWeek)
                .IsRequired();
                entity.Property(b => b.CreationDate)
                .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Models.Schedule>()
            .HasKey(t => t.Id)
            .IsClustered();

            modelBuilder.Entity<Models.Schedule>()
                .HasOne(e => e.Activity)
                .WithMany(e => e.ScheduleRelation)
                .HasForeignKey(e => e.ActivityId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_sch_act");

        }

    }

}