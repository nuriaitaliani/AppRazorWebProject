using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppRazorWeb.Framework.Miscellaneous
{
    public class DependencyInjectionHelpersUser
    {
        /// <summary>
        /// Configure Dependency Injection
        /// </summary>
        /// <param name="services">Service collection</param>
        
        public static void ConfigureDI(IServiceCollection services,
            string connectionString)
        {
            ConfigureUserDI(services, connectionString);
        }

        #region Configure DI's

        #region User

        /// <summary>
        /// Configure user query repository
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void ConfigureUserQueryRepositoryDI(IServiceCollection services)
        {
            services.TryAddSingleton<Repositories.IUsersRepository,
                Repositories.SqlServer.UserRepository>();
        }

        /// <summary>
        /// Configure user data service dependency injection
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void ConfigureUserDataServiceDI(IServiceCollection services)
        {
            ConfigureUserQueryRepositoryDI(services);

            services.TryAddTransient<Dataservices.IUserDataService,
                Dataservices.UserDataService>();
        }

        /// <summary>
        /// Configure user dependency injection
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="options">DbContextOptionsBuilder</param>
        public static void ConfigureUserDI(IServiceCollection services,
            string connectionString)
        {
            ConfigureUserDataServiceDI(services);

            services.TryAddTransient<BusinessService.IUserBusinessService>(z =>
            new BusinessService.UserBusinessService(
                z.GetRequiredService<Dataservices.IUserDataService>(),
                connectionString));
        }

        #endregion User

        #endregion Configure DI's


    }
}
