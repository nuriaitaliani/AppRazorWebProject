
namespace AppRazorWeb.API.Miscellaneous.Models
{
    /// <summary>
    /// WebApi configurations
    /// </summary>
    public class WebApiConfigurations
    {
        /// <summary>
        /// Database settings
        /// </summary>
        public DatabaseSettings DatabaseSettings { get; set; }
    }

    /// <summary>
    /// Database settings
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
