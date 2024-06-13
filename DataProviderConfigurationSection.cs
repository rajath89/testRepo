using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DataProviderConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("defaultProvider", IsRequired = true)]
        public string DefaultProvider
        {
            get { return (string)this["defaultProvider"]; }
            set { this["defaultProvider"] = value; }
        }

        [ConfigurationProperty("providers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ProviderSettingsCollection), AddItemName = "add")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)this["providers"]; }
        }
    }

    public class ProviderSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderSettings)element).Name;
        }

        public new ProviderSettings this[string name]
        {
            get { return (ProviderSettings)BaseGet(name); }
        }
    }

    public class ProviderSettings : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("serviceUrl", IsRequired = false)]
        public string ServiceUrl
        {
            get { return (string)this["serviceUrl"]; }
            set { this["serviceUrl"] = value; }
        }

        [ConfigurationProperty("uddiEnabled", IsRequired = false, DefaultValue = false)]
        public bool UddiEnabled
        {
            get { return (bool)this["uddiEnabled"]; }
            set { this["uddiEnabled"] = value; }
        }

        [ConfigurationProperty("useClientCertificate", IsRequired = false, DefaultValue = false)]
        public bool UseClientCertificate
        {
            get { return (bool)this["useClientCertificate"]; }
            set { this["useClientCertificate"] = value; }
        }

        [ConfigurationProperty("serviceOperationTimeout", IsRequired = false, DefaultValue = 30)]
        public int ServiceOperationTimeout
        {
            get { return (int)this["serviceOperationTimeout"]; }
            set { this["serviceOperationTimeout"] = value; }
        }
    }
}
