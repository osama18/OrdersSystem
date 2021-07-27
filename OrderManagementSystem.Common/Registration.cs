using OrderManagementSystem.Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Common.Loggers;
using OrderManagementSystem.Common.Settings;

namespace OrderManagementSystem.Common.Logging
{
    public static class Registration
    {
        public static IServiceCollection RegisterCommon(this IServiceCollection collection)
        {
            collection.AddSingleton<ISettingProvider, ConfigSettingsProvider>();
            //collection.AddSingleton<IObjectMapper, ObjectMapper>();
            return collection.AddSingleton<ILogger, Logger>();
        }
    }
}
