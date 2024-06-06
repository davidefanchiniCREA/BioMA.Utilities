using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Represents the configuration settings for the mapper.
    /// </summary>
    public class MapperConfigurationSettings : ConfigurationSection
    {
        [ConfigurationProperty("MapperConfigurationSetting")]
        public HashList MappingItems
        {
            get { return ((HashList)(base["MapperConfigurationSetting"])); }
        }
    }

    /// <summary>
    /// Represents a collection of mapper configuration settings.
    /// </summary>
    [ConfigurationCollection(typeof(MapperConfigurationSetting))]
    public class HashList : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MapperConfigurationSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MapperConfigurationSetting)(element)).name;
        }

        public MapperConfigurationSetting this[int idx]
        {
            get
            {
                return (MapperConfigurationSetting)BaseGet(idx);
            }
        }
    }
}
