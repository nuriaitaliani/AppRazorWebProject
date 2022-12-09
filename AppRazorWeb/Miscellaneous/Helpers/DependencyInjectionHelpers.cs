using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ResultCommunication;
using AppRazorWeb.API.Miscellaneous.Models;

namespace AppRazorWeb.API.Miscellaneous.Helpers
{
    /// <summary>
    /// Dependency injection helpers
    /// </summary>
    public class DependencyInjectionHelpers
    {

        #region Public methods

        /// <summary>
        /// Configure dependency injection
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="services">Service collection</param>
        /// <returns>The <see cref="ExecutionResult"/></returns>
        public static IExecutionResult ConfigureDI(IConfiguration configuration,
            IServiceCollection services)
        {
            ConfigureDataService(services);

            services.TryAddTransient<Framework.Dataservices.IConnectionCrud,
                Framework.DataServices.ConnectionCrud>();

            WebApiConfigurations configurations = GetWebApiConfigurations(configuration);
            services.TryAddSingleton(configurations);

            Framework.Miscellaneous.DependencyInjectionHelpersActivity.ConfigureDI(services, configurations.DatabaseSettings.ConnectionString);
            Framework.Miscellaneous.DependencyInjectionHelpersSchedule.ConfigureDI(services, configurations.DatabaseSettings.ConnectionString);
            Framework.Miscellaneous.DependencyInjectionHelpersUser.ConfigureDI(services, configurations.DatabaseSettings.ConnectionString);

            return new ExecutionResult();
        }

        #endregion Public methods

        #region Private methods

        #region Configure data service

        /// <summary>
        /// Dataservice API configuration
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureDataService(IServiceCollection services)
        {
            
        }

        #endregion Configure data service

        #region Webapi configurations

        /// <summary>
        /// Get the web api configurations
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>The <see cref="WebApiConfigurations"/></returns>
        private static WebApiConfigurations GetWebApiConfigurations(IConfiguration configuration)
        {

            DatabaseSettings databaseSettings = new DatabaseSettings();
            configuration.GetSection("DatabaseSettings").Bind(databaseSettings);

            WebApiConfigurations configurations = new WebApiConfigurations()
            {
                DatabaseSettings = databaseSettings,
            };

            return configurations;
        }

        #endregion Webapi configurations

        #endregion Private methods

    }
}
