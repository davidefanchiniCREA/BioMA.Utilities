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
    public class MapperConfigurationSetting : ConfigurationElement, IConfigurationSectionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperConfigurationSetting"/> class.
        /// </summary>
        public MapperConfigurationSetting()
        {
        }

        /// <summary>
        /// Gets or sets the value of the configuration setting.
        /// </summary>
        [ConfigurationProperty("value", IsKey = false, IsRequired = true)]
        public string value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the configuration setting.
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        #region IConfigurationSectionHandler Members

        /// <summary>
        /// Creates a new instance of the <see cref="MapperConfigurationSetting"/> class.
        /// </summary>
        /// <param name="parent">The parent object.</param>
        /// <param name="configContext">The configuration context.</param>
        /// <param name="section">The XML node representing the configuration section.</param>
        /// <returns>An instance of the <see cref="MapperConfigurationSetting"/> class.</returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            MapperConfigurationSetting m = new MapperConfigurationSetting();

            return m;
        }

        #endregion
    }
}
