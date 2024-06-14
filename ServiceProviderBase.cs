using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class ServiceProviderBase
    {
        public DataProviderConfigurationSection Config { get; set; }

        protected T CreateProxyInterface<T>()
        {
            string defaultProviderName = Config.DefaultProvider;
            var providerSettings = Config.Providers[defaultProviderName];

            string endpointAddress = providerSettings.ServiceUrl;
            int operationTimeout = providerSettings.ServiceOperationTimeout;

            var binding = new BasicHttpBinding
            {
                OpenTimeout = TimeSpan.FromSeconds(operationTimeout),
                CloseTimeout = TimeSpan.FromSeconds(operationTimeout),
                SendTimeout = TimeSpan.FromSeconds(operationTimeout),
                ReceiveTimeout = TimeSpan.FromSeconds(operationTimeout)
            };

            var endpoint = new EndpointAddress(endpointAddress);
            var channelFactory = new ChannelFactory<T>(binding, endpoint);
            return channelFactory.CreateChannel();
        }

        protected void DisposeProxy<T>(T proxy)
        {
            if (proxy is IClientChannel clientChannel)
            {
                try
                {
                    if (clientChannel.State == CommunicationState.Faulted)
                    {
                        clientChannel.Abort();
                    }
                    else
                    {
                        clientChannel.Close();
                    }
                }
                catch
                {
                    clientChannel.Abort();
                }
            }
        }
    }
}
