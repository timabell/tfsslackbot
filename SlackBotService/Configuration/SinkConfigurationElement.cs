using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Configuration
{
    /// <summary>
    /// Represents configuration about a message sink.
    /// </summary>
    public class SinkConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the name of the sink.
        /// </summary>
        /// <value>
        /// The name of the sink.
        /// </value>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        /// <summary>
        /// Gets the type of the sink.
        /// </summary>
        /// <value>
        /// The type of the sink.
        /// </value>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)base["type"]; }
        }
    }
}
