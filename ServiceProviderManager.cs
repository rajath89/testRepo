using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public static class ServiceProviderManager<T> where T : ServiceProviderBase
    {
        public static T Provider { get; private set; }
        public static DataProviderConfigurationSection ServiceConfig { get; private set; }

        static ServiceProviderManager()
        {
            var test = ConfigurationManager.GetSection("OAPortfolioService");
            ServiceConfig = (DataProviderConfigurationSection)ConfigurationManager.GetSection("OAPortfolioService");
            if (ServiceConfig == null)
            {
                throw new ConfigurationErrorsException("OAPortfolioService section not found in configuration.");
            }

            var defaultProviderName = ServiceConfig.DefaultProvider;
            var providerSettings = ServiceConfig.Providers[defaultProviderName];

            if (providerSettings == null)
            {
                throw new ConfigurationErrorsException($"Provider settings for {defaultProviderName} not found.");
            }

            var providerType = Type.GetType(providerSettings.Type);

            if (providerType == null)
            {
                throw new ConfigurationErrorsException($"Unable to find type {providerSettings.Type}.");
            }

            Provider = (T)Activator.CreateInstance(providerType);

            if (Provider == null)
            {
                throw new ConfigurationErrorsException($"Unable to create instance of type {providerSettings.Type}.");
            }

            Provider.Config = ServiceConfig;
        }
    }

}
