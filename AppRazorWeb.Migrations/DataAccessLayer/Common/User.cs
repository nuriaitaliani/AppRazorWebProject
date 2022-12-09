using Microsoft.EntityFrameworkCore;

namespace AppRazorWeb.Migrations.DataAccessLayer.Common
{
    public class Users : DbContext
    {

        public static void GetTableDefinitions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>(entity =>
            {

                entity.Property(b => b.Name)
                .HasMaxLength(56)
                .IsRequired();
                entity.Property(b => b.LastName)
                .HasMaxLength(56)
                .IsRequired();
                entity.Property(b => b.DNI)
                .HasMaxLength(16)
                .IsRequired();
                entity.Property(b => b.Age)
                .IsRequired();
                entity.Property(b => b.PhoneNumber)
                .HasMaxLength(16)
                .IsRequired();
                entity.Property(b => b.Email)
                .HasMaxLength(56)
                .IsRequired();
                entity.Property(b => b.CreationDate)
                .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Models.User>()
                .HasKey(t => t.Id)
                .IsClustered();

            modelBuilder.Entity<Models.User>()
                .HasAlternateKey(t => t.DNI)
                .IsClustered(false);


        }

    }
}
