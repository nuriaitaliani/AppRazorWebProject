using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppRazorWeb.Framework.Miscellaneous
{
    /// <summary>
    /// Dependency injection helper methods
    /// </summary>
    public class DependencyInjectionHelpersActivity
    {

        /// <summary>
        /// Configure Dependency Injection
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="dbContextOptionsBuilder">DbContextOptionsHelper</param>
        public static void ConfigureDI(IServiceCollection services,
            string connectionString)
        {
            ConfigureActivityDI(services, connectionString);
        }

        #region Configure DI's

        #region Activity

        /// <summary>
        /// Configure activity query repository
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void ConfigureActivityQueryRepositoryDI(IServiceCollection services)
        {
            services.TryAddSingleton<Repositories.IActivityRepository,
                Repositories.SqlServer.ActivityRepository>();
        }

        /// <summary>
        /// Configure activity data service dependency injection                                       
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void ConfigureActivityDataServiceDI(IServiceCollection services)
        {
            //Necesitas acceso a los repositories en la coleccion services
            ConfigureActivityQueryRepositoryDI(services);

            services.TryAddTransient<Dataservices.IActivityDataService,
                Dataservices.ActivityDataService>();
        }

        /// <summary>
        /// Configure activity dependency injection
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="options">DbContextOptionsBuilder</param>
        public static void ConfigureActivityDI(IServiceCollection services,
            string connectionString)
        {
            //Necesitas acceso a los dataServices
            ConfigureActivityDataServiceDI(services);
            DependencyInjectionHelpersSchedule.ConfigureScheduleDataServiceDI(services);

            services.TryAddTransient<BusinessService.IActivityBusinessService>(z =>
            new BusinessService.ActivityBusinessService(
                z.GetRequiredService<Dataservices.IActivityDataService>(),
                z.GetRequiredService<Dataservices.IScheduleDataService>(),
                connectionString));

        }

        #endregion Activity

        #endregion Configure DI's

    }
}
