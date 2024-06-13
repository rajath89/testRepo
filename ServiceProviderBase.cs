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
            // Assuming the endpoint address is fetched from the config
            string endpointAddress = Config.Providers[Config.DefaultProvider].ServiceUrl;
            var binding = new BasicHttpBinding();
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
