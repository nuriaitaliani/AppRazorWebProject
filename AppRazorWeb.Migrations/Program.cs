using AppRazorWeb.Migrations.DataAccessLayer.SqlServer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace AppRazorWeb.Migrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "configuration.json");
            System.IO.File.ReadAllText(jsonFilePath);
            Settings settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(jsonFilePath));

            DevelopmentSqlServerDbContext dbContext = new DevelopmentSqlServerDbContext();///////////// Estos 2 son necesarios para crear las migraciones y conectar con la DB

            dbContext.Database.SetConnectionString(settings.ConnectionString);

            dbContext.Database.Migrate();////////////////////////////////////////////////
        }
    }
}
