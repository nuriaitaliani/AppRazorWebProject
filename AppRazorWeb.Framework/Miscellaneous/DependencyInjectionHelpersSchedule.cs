using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppRazorWeb.Framework.Miscellaneous
{
    public class DependencyInjectionHelpersSchedule
    {
        /// <summary>
        /// Configure Dependency Injection
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="dbContextOptionsBuilder">DbContextOptionsHelper</param>
        public static void ConfigureDI(IServiceCollection services,
            string connectionString)
        {
            ConfigureScheduleDI(services, connectionString); 
        }

        #region Configure DI's

        #region Schedule

        /// <summary>
        /// Configure schedule query repository
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void ConfigureScheduleQueryRepositoryDI(IServiceCollection services)
        {
            services.TryAddSingleton<Repositories.IScheduleRepository,
                Repositories.SqlServer.ScheduleRepository>();
        }

        /// <summary>
        /// Configure schedule data service dependency injection
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void ConfigureScheduleDataServiceDI(IServiceCollection services)
        {
            ConfigureScheduleQueryRepositoryDI(services);

            services.TryAddTransient<Dataservices.IScheduleDataService,
                Dataservices.ScheduleDataService>();
        }

        public static void ConfigureScheduleDI(IServiceCollection services,
            string connectionString)
        {
            ConfigureScheduleDataServiceDI(services);
            DependencyInjectionHelpersActivity.ConfigureActivityDataServiceDI(services);

            services.TryAddTransient<BusinessService.IScheduleBusinessService>(z =>
            new BusinessService.ScheduleBusinessService(
                z.GetRequiredService<Dataservices.IScheduleDataService>(),
                z.GetRequiredService<Dataservices.IActivityDataService>(),
                z.GetRequiredService<Dataservices.IUserScheduleDataService>(),
                z.GetRequiredService<Dataservices.IUserDataService>(),
                connectionString));            
        }

        #endregion Schedule

        #endregion Configure DI's

    }
}
