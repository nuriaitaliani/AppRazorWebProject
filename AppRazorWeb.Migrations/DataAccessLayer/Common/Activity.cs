using Microsoft.EntityFrameworkCore;

namespace AppRazorWeb.Migrations.DataAccessLayer.Common
{
    public class Activities : DbContext
    {

        public static void GetTableDefinitions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Activity>(entity =>
            {
                entity.Property(b => b.Name)
                .HasMaxLength(56)
                .IsRequired();

                entity.Property(b => b.Description)
                .HasMaxLength(144);

                entity.Property(b => b.Place)
                .HasMaxLength(56);

                entity.Property(b => b.StudentName)
                .HasMaxLength(56);

                entity.Property(b => b.DailyLesson)
                .HasMaxLength(56);

                entity.Property(b => b.CreationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            });

            modelBuilder.Entity<Models.Activity>()
                .HasKey(t => t.Id)
                .IsClustered();
        }

    }
}
